using Microsoft.AspNetCore.Components;
using Notes.Models;

namespace Notes.Components;

public partial class Card
{
    [Parameter,EditorRequired] public ParentNotes ParentNote { get; set; } = null!;
    [Parameter,EditorRequired] public EventCallback<TakeAction<ParentNotes>> TakeActionOnMe { get; set; }  
    
    bool _isEditing = false;
    bool _isDeleting = false;  
    private void EditNode()
    {
        Console.WriteLine("edit Clicked" + DateTime.Now);
        _isEditing = true;
    }
    private async Task EditedNode(ParentNotes? parentNotes)
    {
        if (parentNotes is not null)
        {
            await TakeActionOnMe.InvokeAsync( new TakeAction<ParentNotes>()
            {
                CrudType = ActionType.Update,
                ObjectData = parentNotes
            });
        }
        _isEditing = false;
    }
    private async  Task DeletedNode(string? isDeleted)
    {
        _isDeleting = false;
        if(isDeleted is null) return;
        await TakeActionOnMe.InvokeAsync( new TakeAction<ParentNotes>()
        {
            CrudType = ActionType.Delete,
            ObjectData = ParentNote
        });
    }
    private void DeletePrompt()
    {
        _isDeleting = true;
    }
    private void EditPrompt()
    {
        _isEditing = true;
    }
}