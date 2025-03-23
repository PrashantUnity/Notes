namespace Notes.Models;

public class ChildNote
{
    public string Name { get; set; }
    public NoteType NoteTypes { get; set; } = NoteType.Code;
    public string Text { get; set; }
    public string ProgrammingLanguage { get; set; } = ProgrammingLanguages.Languages.First();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string TruncatedText(int length) 
        => Text.Length > length ? $"{Text[..length]}..." : Text;
}