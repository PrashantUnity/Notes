namespace Notes.Services;

public class ToastService
{
    public event Action<string, string, int>? OnShow;

    public void ShowToast(string message, string type = "info", int duration = 3000)
    {
        OnShow?.Invoke(message, type, duration);
    }
}