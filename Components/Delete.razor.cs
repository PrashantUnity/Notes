using Microsoft.AspNetCore.Components;
using MudBlazor;
using Notes.Models;

namespace Notes.Components;

public partial class Delete
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] public ParentNotes ParentNote { get; set; } = null!;

    [Parameter] public string ContentText { get; set; } = null!;

    [Parameter] public string ButtonText { get; set; } = null!;

    [Parameter] public Color Color { get; set; }

    private void Submit() => MudDialog.Close(DialogResult.Ok(true));

    private void Cancel() => MudDialog.Cancel();
}