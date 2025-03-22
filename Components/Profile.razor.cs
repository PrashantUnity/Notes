using Microsoft.AspNetCore.Components;

namespace Notes.Components;

public partial class Profile 
{
    [Parameter] public EventCallback HideProfile { get; set; }
    
    private async Task CloseButtonClicked()
    {
        await HideProfile.InvokeAsync();
    }
}