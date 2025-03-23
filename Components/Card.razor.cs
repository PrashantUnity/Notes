using Microsoft.AspNetCore.Components;
using MudBlazor;
using Notes.Models;

namespace Notes.Components;

public partial class Card
{
    [Parameter, EditorRequired] public ParentNotes ParentNote { get; set; } = null!;
    [Parameter, EditorRequired] public EventCallback<TakeAction<ParentNotes>> TakeActionOnMe { get; set; }

    private async Task DeletePrompt()
    {
        DialogOptions topCenter = new() { Position = DialogPosition.TopCenter };
        var parameters = new DialogParameters<Delete>
        {
            { x => x.ParentNote, ParentNote },
            { x => x.ContentText, "Do you really want to delete these records? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };
        var dialog = await DialogService.ShowAsync<Delete>("Delete", parameters, topCenter);
        var result = await dialog.Result;
        if (result is { Canceled: false })
        {
            await TakeActionOnMe.InvokeAsync(new TakeAction<ParentNotes>()
            {
                CrudType = ActionType.Delete,
                ObjectData = ParentNote
            });
        }
    }

    private async Task EditPrompt()
    {
        var parameters = new DialogParameters<EditCard> { { x => x.ExistingNote, ParentNote } };
        var option = new DialogOptions
        {
            MaxWidth = MaxWidth.Large,
            FullWidth = true,
            CloseButton = true
        };
        var dialog = await DialogService.ShowAsync<EditCard>("Edit", parameters, option);
        var result = await dialog.Result;

        if (result is { Canceled: false })
        {
            var data = (ParentNotes)result.Data!;
            await TakeActionOnMe.InvokeAsync(new TakeAction<ParentNotes>()
            {
                CrudType = ActionType.Update,
                ObjectData = data
            });
        }
    }
}