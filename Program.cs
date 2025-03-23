using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Notes;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://accounts.google.com";
    options.ProviderOptions.ClientId = "315199970520-h1el19srk4vgtnuu0qmv3avsgqul6ccs.apps.googleusercontent.com";

    if (builder.HostEnvironment.IsDevelopment())
    {
        options.ProviderOptions.RedirectUri = "https://localhost:7156/authentication/login-callback";
    }
    else
    {
        options.ProviderOptions.RedirectUri = "https://prashantunity.github.io/Notes/authentication/login-callback";
    }

    options.ProviderOptions.DefaultScopes.Add("email");
});
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("AnonymousAccess", policy => policy.RequireAssertion(context => true));
});
builder.Services.AddMudServices();
await builder.Build().RunAsync();