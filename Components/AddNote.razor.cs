using System.Text.Json;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Notes.Models;
using Notes.Utilities;

namespace Notes.Components;

public partial class AddNote
{
     
    [CascadingParameter]
    private IMudDialogInstance MudDialog { get; set; } = null!;  
    private ParentNotes _parentNotes = new();
    ChildNote _childNote = new();
    private List<ChildNote> _childNotes = [];
    private void AddChildNote()
    {
        _childNotes.Add(_childNote);
        _childNote = new ChildNote();
    }
    private async Task SummitButtonClicked()
    {
        if(Helper.User == null || string.IsNullOrWhiteSpace(_parentNotes.Name))
        {
            //ShowToast("Please login to add a note!", Severity.Error);
            StateHasChanged();
            return;
        }
        _parentNotes.CreatedAt = DateTime.Now;
        _parentNotes.CodeNotes = _childNotes;
        var formData = new
        {
            name = _parentNotes.Name,
            email = Helper.User.Email,
            description = JsonSerializer.Serialize(_parentNotes)
        };

        var result = await JsRuntime.InvokeAsync<SaveResult>("saveContact",[Helper.User.Email, formData]);

        if (result.Success)
        {
            _parentNotes = new ParentNotes(); 
            Snackbar.Add("Data saved successfully!", Severity.Success);
        }
        else
        {
            Snackbar.Add("Data Failed To save !",  Severity.Error);
        }

        MudDialog.Close(DialogResult.Ok(true));
    }
    private void Cancel() => MudDialog.Cancel();  
}