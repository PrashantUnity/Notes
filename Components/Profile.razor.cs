using Microsoft.AspNetCore.Components;
using MudBlazor;
using Notes.Models;

namespace Notes.Components;

public partial class Profile 
{
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public User ProfileData { get; set; } = null!;

    private void Submit() => MudDialog.Close(DialogResult.Ok(true)); 
}