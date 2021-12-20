namespace ReduxSimple.Tests.Setup.TodoListStore;

public record TodoListState
{
    public ImmutableList<TodoItem>? TodoList { get; set; }
    public string? CurrentUser { get; set; }
    public string? UselessProperty { get; set; }
}
