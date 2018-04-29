using System.Collections.Immutable;

namespace ReduxSimple.UnitTests
{
    public static class Functions
    {
        public static TodoListState CreateInitialTodoListState()
        {
            return new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
        }

        public static void DispatchAddTodoItemAction<T>(ReduxStore<T> store, int id, string title) where T : class, new()
        {
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = id,
                    Title = title
                }
            });
        }

        public static void DispatchSwitchUserAction<T>(ReduxStore<T> store, string newUser) where T : class, new()
        {
            store.Dispatch(new SwitchUserAction
            {
                NewUser = newUser
            });
        }

        public static TodoListState ReduceTodoListState(TodoListState state, object action)
        {
            if (action is AddTodoItemAction addTodoItemAction)
            {
                var newState = new TodoListState
                {
                    TodoList = state.TodoList.Add(addTodoItemAction.TodoItem),
                    CurrentUser = state.CurrentUser
                };
                return newState;
            }
            if (action is SwitchUserAction switchUserAction)
            {
                var newState = new TodoListState
                {
                    TodoList = state.TodoList,
                    CurrentUser = switchUserAction.NewUser
                };
                return newState;
            }

            return null;
        }

        public static void DispatchAllActions<T>(ReduxStore<T> store) where T : class, new()
        {
            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");
            DispatchAddTodoItemAction(store, 2, "Create Models");
            DispatchAddTodoItemAction(store, 3, "Refactor tests");
        }
    }
}
