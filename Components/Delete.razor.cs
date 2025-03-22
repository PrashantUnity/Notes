using Microsoft.AspNetCore.Components;
using Notes.Models;

namespace Notes.Components;

public partial class Delete
{
    [Parameter] public EventCallback<string> DeleteNode { get; set; }
    
    [Parameter] public ParentNotes? ParentNote { get; set; }
    
    private async Task DeleteButtonClicked()
    {
        await DeleteNode.InvokeAsync(ParentNote?.Id);
    }
    private async Task CloseButtonClicked()
    {
        await DeleteNode.InvokeAsync(string.Empty);
    }
}