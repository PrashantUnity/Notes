
using Notes.Models;

namespace Notes.Utilities;

public enum ToastSeverity { Success, Error, Info, Warning }

public class ToastMessage
{
    public string Message { get; set; } = "";
    public ToastSeverity Severity { get; set; }
    public int Duration { get; set; } = 3000;
}

public class ToastService
{
    public event Action<ToastService>? OnShow;

    public void ShowToast(string message, ToastSeverity severity = ToastSeverity.Info, int duration = 3000)
    {
        OnShow?.Invoke(new ToastService { Message = message, Severity = severity, Duration = duration });
    }
}