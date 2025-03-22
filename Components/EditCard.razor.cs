using Microsoft.AspNetCore.Components;
using Notes.Models;

namespace Notes.Components;

public partial class EditCard
{
    [Parameter]
    public EventCallback<ParentNotes> EditNode { get; set; }
    [Parameter] public ParentNotes ParentNote { get; set; } = null!;
    
    private async Task SummitButtonClicked()
    {
        await EditNode.InvokeAsync(ParentNote);
    }
    private async Task CloseButtonClicked()
    {
        await EditNode.InvokeAsync(ParentNote);
    }
}