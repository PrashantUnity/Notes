using System.Text.Json;
using MudBlazor;
using Notes.Models;
using Notes.Utilities;

namespace Notes.Components;

public partial class AllCards
{
    
    bool _isNewNoteAdding = false;

    private Task ActionOnCard(TakeAction<ParentNotes>? takeAction)
    {
        if (takeAction == null)
        {
            return Task.CompletedTask;
        }

        _ = takeAction.CrudType switch
        {
            ActionType.Delete => DeleteCard(takeAction),
            ActionType.Update => UpdateCard(takeAction),
            _ => Task.CompletedTask
        };
        return Task.CompletedTask;
    }

    private void NewNote()
    {
        _isNewNoteAdding = true;
    }

    private async Task AddedNewNote()
    {
        _isNewNoteAdding = false;
        await LoadAllData();
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        if (Helper.ParentsNodes.Count == 0)
        {
            await LoadAllData();
        }
    }

    private async Task LoadAllData()
    {
        if (Helper.User == null) return; 
        try
        { 
            var result = await JsRuntime.InvokeAsync<JsonElement>("getContacts", args: [Helper.User.Email]);
            var snippets = new List<ParentNotes>();

            foreach (var contactElement in result.EnumerateArray())
            {
                var contact = new DataNode
                {
                    Id = contactElement.GetProperty("id").GetString() ?? throw new InvalidOperationException(),
                    Name = contactElement.GetProperty("name").GetString()!,
                    Email = contactElement.GetProperty("email").GetString() ?? throw new InvalidOperationException(),
                    Description = contactElement.GetProperty("description").GetString()!,
                    Timestamp = Helper.ParseFirestoreTimestamp(contactElement.GetProperty("timestamp"))
                };
                var parentNode = JsonSerializer.Deserialize<ParentNotes>(contact?.Description ?? string.Empty);
                if (parentNode == null) continue;
                if (contact != null) parentNode.Id = contact.Id;

                snippets.Add(parentNode);
            }

            Helper.ParentsNodes = snippets.OrderByDescending(c => c.CreatedAt).ToList();
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Error loading contacts: {ex.Message}", Severity.Error);
        }
    }

    private async Task DeleteCard(TakeAction<ParentNotes>? action)
    {
        if (action == null || string.IsNullOrEmpty(action.ObjectData.Id) || Helper.User == null)
        {
            return;
        }

        var result = await JsRuntime.InvokeAsync<SaveResult>("deleteContact", args: [Helper.User.Email, action.ObjectData.Id]);

        if (result.Success)
        {
            Snackbar.Add("Contact deleted successfully", Severity.Success);
            await UpdateData();
        }
        else
        {
            Snackbar.Add($"Delete failed: {result.Error}", Severity.Error);
        }
    }

    private async Task UpdateCard(TakeAction<ParentNotes>? action)
    {
        if (action == null || string.IsNullOrEmpty(action.ObjectData.Id) || Helper.User == null)
        {
            return;
        }

        var formData = new
        {
            name = action.ObjectData.Name,
            email = Helper.User.Email,
            description = JsonSerializer.Serialize(action.ObjectData)
        };
        var result = await JsRuntime.InvokeAsync<SaveResult>("updateContact", args: [Helper.User.Email, action.ObjectData.Id, formData]);

        if (result.Success)
        {
            Snackbar.Add("Contact Updated successfully", Severity.Success);
            await UpdateData();
        }
        else
        {
            Snackbar.Add($"Delete failed: {result.Error}", Severity.Error);
        }
    }

    private async Task UpdateData()
    {
        await LoadAllData();
        StateHasChanged();
    }
}