namespace Notes.Models;

public class ChildNote
{
    public required string Name { get; set; }
    public NoteType NoteTypes { get; set; } = NoteType.Code;
    public required string Text { get; set; }
    public string ProgrammingLanguage { get; set; } = ProgrammingLanguages.Cs;
}