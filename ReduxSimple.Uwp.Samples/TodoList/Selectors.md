```csharp
public static class Selectors
{
    public static ISelectorWithoutProps<RootState, TodoListState> SelectTodoListState = CreateSelector(
        (RootState state) => state.TodoList
    );

    private static readonly ISelectorWithoutProps<RootState, TodoItemEntityState> SelectItemsEntityState = CreateSelector(
        SelectTodoListState,
        state => state.Items
    );
    private static readonly EntitySelectors<RootState, int, TodoItem> TodoItemSelectors = TodoItemAdapter.GetSelectors(SelectItemsEntityState);

    public static ISelectorWithoutProps<RootState, TodoFilter> SelectFilter = CreateSelector(
        SelectTodoListState,
        state => state.Filter
    );
    public static ISelectorWithoutProps<RootState, List<TodoItem>> SelectItems = TodoItemSelectors.SelectEntities;
}
```