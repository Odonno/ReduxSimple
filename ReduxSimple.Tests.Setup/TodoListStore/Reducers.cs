using Converto;
using System.Collections.Generic;
using static ReduxSimple.Reducers;

namespace ReduxSimple.Tests.Setup.TodoListStore
{
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

                        return state.With(
                            new
                            {
                                TodoList = state.TodoList?.Add(action.TodoItem)
                            }
                        );
                    }
                ),
                On<SwitchUserAction, TodoListState>(
                    (state, action) => state.With(
                        new
                        {
                            CurrentUser = action.NewUser
                        }
                    )
                )
            };
        }
    }
}
