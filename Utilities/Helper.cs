using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Notes.Models;

namespace Notes.Utilities;

public class Helper
{
    public static User? User { get; set; }
    [Inject] static IJSRuntime JSRuntime { get; set; } = null!;
    public static List<ParentNotes> ParentsNodes { get; set; } = [];

    public static DateTime ParseFirestoreTimestamp(JsonElement timestampElement)
    {
        try
        {
            var seconds = timestampElement.GetProperty("seconds").GetInt64();
            var nanoseconds = timestampElement.GetProperty("nanoseconds").GetInt64();
            return DateTimeOffset.FromUnixTimeSeconds(seconds)
                .AddTicks(nanoseconds / 100)
                .DateTime
                .ToLocalTime();
        }
        catch
        {
            return DateTime.MinValue;
        }
    }
}