namespace Notes.Models;

public class ParentNotes
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now; 
    public List<ChildNote> CodeNotes { get; set; } = [];
}