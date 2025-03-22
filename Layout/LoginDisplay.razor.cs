using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Notes.Utilities;

namespace Notes.Layout;

public partial class LoginDisplay
{
    bool _showProfile = false;
    public void BeginLogOut()
    {
        Helper.User = null;
        Navigation.NavigateToLogout("authentication/logout","/");
    }

    private void HideMe()
    {
        _showProfile = false;
    }
    private void ShowProfile()
    {
        _showProfile = true;
    }
}