using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor;
using Notes.Components;
using Notes.Utilities;

namespace Notes.Layout;

public partial class LoginDisplay
{
    public void BeginLogOut()
    {
        Helper.User = null;
        Navigation.NavigateToLogout("authentication/logout","/");
    } 
    private async Task ShowProfile()
    {
        var parameters = new DialogParameters<Profile> { { x => x.ProfileData, Helper.User } };
        var option = new DialogOptions
        {
            NoHeader =  true
        };
        var dialog = await DialogService.ShowAsync<Profile>("Profile", parameters,option);
        var result = await dialog.Result;
         
        if( result is { Canceled: false })
        { 
            // late Maybe we can do something here
        } 
    }
}