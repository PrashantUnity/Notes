namespace Notes.Models;

public class TakeAction<T>
{
    public ActionType CrudType { get; set; } = ActionType.Create;
    public T ObjectData { get; set; }
}

public enum ActionType
{
    Create,
    Update,
    Delete
}