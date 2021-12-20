using static ReduxSimple.Reducers;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;

namespace ReduxSimple.Tests.Setup.TodoListStore;

public static class Reducers
{
    public static IEnumerable<On<TodoListState>> CreateReducers()
    {
        return new List<On<TodoListState>>
        {
            On<AddTodoItemAction, TodoListState>(
                (state, action) =>
                {
                    if (action.TodoItem == null)
                        return state;

                    return state with
                    {
                        TodoList = state.TodoList?.Add(action.TodoItem)
                    };
                }
            ),
            On<SwitchUserAction, TodoListState>(
                (state, action) => state with
                {
                    CurrentUser = action.NewUser
                }
            ),
            On<ResetStateAction, TodoListState>(
                (state, action) => CreateInitialTodoListState()
            )
        };
    }
}
