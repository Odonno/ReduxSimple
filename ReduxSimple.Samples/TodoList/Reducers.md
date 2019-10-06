```csharp
public static class Reducers
{
    public static IEnumerable<On<TodoListState>> CreateReducers()
    {
        return new List<On<TodoListState>>
        {
            On<SetFilterAction, TodoListState>(
                (state, action) => state.With(new { action.Filter })
            ),
            On<CreateTodoItemAction, TodoListState>(
                state =>
                {
                    int newId = state.Items.Collection.Any() ? state.Items.Ids.Max() + 1 : 1;
                    return state.With(new
                    {
                        Items = TodoItemAdapter.UpsertOne(new TodoItem { Id = newId }, state.Items)
                    });
                }
            ),
            On<CompleteTodoItemAction, TodoListState>(
                (state, action) =>
                {
                    return state.With(new
                    {
                        Items = TodoItemAdapter.UpsertOne(new { action.Id, Completed = true }, state.Items)
                    });
                }
            ),
            On<RevertCompleteTodoItemAction, TodoListState>(
                (state, action) =>
                {
                    return state.With(new
                    {
                        Items = TodoItemAdapter.UpsertOne(new { action.Id, Completed = false }, state.Items)
                    });
                }
            ),
            On<UpdateTodoItemAction, TodoListState>(
                (state, action) =>
                {
                    return state.With(new
                    {
                        Items = TodoItemAdapter.UpsertOne(new { action.Id, action.Content }, state.Items)
                    });
                }
            ),
            On<RemoveTodoItemAction, TodoListState>(
                (state, action) =>
                {
                    return state.With(new
                    {
                        Items = TodoItemAdapter.RemoveOne(action.Id, state.Items)
                    });
                }
            )
        };
    }
}
```