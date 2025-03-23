using System.Text.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Notes.Models;
using Notes.Utilities;

namespace Notes.Components;

public partial class EditCard
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public ParentNotes ExistingNote { get; set; } = null!;

    private ParentNotes _parentNotes = new();
    private List<ChildNote> _childNotes = [];

    protected override void OnInitialized()
    {
        _parentNotes = ExistingNote;
        _childNotes = new List<ChildNote>(_parentNotes.CodeNotes);
    }

    private async Task SaveChanges()
    {
        if (Helper.User == null || string.IsNullOrWhiteSpace(_parentNotes.Name))
        {
            StateHasChanged();
            return;
        }

        _parentNotes.CodeNotes = _childNotes;

        var formData = new
        {
            name = _parentNotes.Name,
            email = Helper.User.Email,
            description = JsonSerializer.Serialize(_parentNotes)
        };

        var result = await JsRuntime.InvokeAsync<SaveResult>("updateNote", [Helper.User.Email, formData]);

        if (result.Success)
        {
            Snackbar.Add("Note updated successfully!", Severity.Success);
        }
        else
        {
            Snackbar.Add("Failed to update note!", Severity.Error);
        }

        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel() => MudDialog.Cancel();
}