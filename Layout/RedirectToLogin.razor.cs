using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Notes.Layout;

public partial class RedirectToLogin 
{
    protected override void OnInitialized()
    {
        Navigation.NavigateToLogin("authentication/login");
    }
}