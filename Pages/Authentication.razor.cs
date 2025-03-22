using Microsoft.AspNetCore.Components;

namespace Notes.Pages;

public partial class Authentication
{
    [Parameter] public string? Action { get; set; }
}