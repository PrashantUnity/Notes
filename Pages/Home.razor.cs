using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Notes.Models;
using Notes.Utilities;

namespace Notes.Pages;

public partial class Home
{
    [CascadingParameter] public Task<AuthenticationState> AuthStateTask { get; set; }
    private ClaimsPrincipal _principal = new();

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateTask;
        _principal = authState.User;
        Helper.User ??= new User();

        foreach (var claim in _principal.Claims)
        { 
            switch (claim.Type)
            {
                case "email":
                    Helper.User.Email = claim.Value;
                    break;
                case "name":
                    Helper.User.Name = claim.Value;
                    break;
                case "picture":
                    Helper.User.Picture = claim.Value;
                    break;
                case "azp":
                    Helper.User.Azp = claim.Value;
                    break;
                case "sub":
                    Helper.User.Sub = claim.Value;
                    break;
                case "email_verified":
                    Helper.User.IsEmailVerified = true;
                    break;
                case "jti":
                    Helper.User.Jti = claim.Value;
                    break;
            }
        }
    }
}