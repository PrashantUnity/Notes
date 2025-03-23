using Microsoft.JSInterop;

namespace Notes.Layout;

public partial class MainLayout
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        var config = new
        {
            apiKey = "AIzaSyCPoEUbf0yiB11Z3Gfba7qpABdKgJBUTGA",
            authDomain = "notetaking-87550.firebaseapp.com",
            projectId = "notetaking-87550",
            storageBucket = "notetaking-87550.firebasestorage.app",
            messagingSenderId = "1067545872761",
            appId = "1:1067545872761:web:01f006ce0d16921c4007a8",
            measurementId = "G-NPR8WQKRD3"
        };
        await JsRuntime.InvokeVoidAsync("initializeFirebase", config);
    }
}