```csharp
public class TodoItem
{
    public int Id { get; set; }
    public string Content { get; set; }
    public bool Completed { get; set; }
}

public enum TodoFilter
{
    All,
    Todo,
    Completed
}

public class TodoListState
{
    public ImmutableList<TodoItem> Items { get; set; } = ImmutableList<TodoItem>.Empty;
    public TodoFilter Filter { get; set; }
}
```