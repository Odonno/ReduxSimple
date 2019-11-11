using System.Collections.Immutable;

namespace ReduxSimple.Tests.Setup.TodoListStore
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
        
        public static void DispatchAllActions<T>(ReduxStore<T> store) where T : class, new()
        {
            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");
            DispatchAddTodoItemAction(store, 2, "Create Models");
            DispatchAddTodoItemAction(store, 3, "Refactor tests");
        }
    }
}
