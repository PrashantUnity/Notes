using System.ComponentModel.DataAnnotations;

namespace Notes.Models;

public class ParentNotes
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Color { get; set; } = "#000000";
    public List<ChildNote> CodeNotes { get; set; } = [];
}