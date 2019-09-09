```csharp
public static class Selectors
{
    public static Func<TodoListState, TodoFilter> SelectFilter = state => state.Filter;
    public static Func<TodoListState, ImmutableList<TodoItem>> SelectItems = state => state.Items;
}
```