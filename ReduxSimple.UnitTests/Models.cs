using System.Collections.Immutable;

namespace ReduxSimple.UnitTests
{
    public class EmptyState
    {
    }

    public class StoreWithEmptyState : ReduxStore<EmptyState>
    {
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
    }

    public class TodoListState
    {
        public ImmutableList<TodoItem> TodoList { get; set; }
        public string CurrentUser { get; set; }
    }

    public class TodoListStore : ReduxStore<TodoListState>
    {
        public TodoListStore(TodoListState initialState = null) : base(initialState)
        {
        }

        protected override TodoListState Reduce(TodoListState state, object action)
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

            return base.Reduce(state, action);
        }
    }

    public class AddTodoItemAction
    {
        public TodoItem TodoItem { get; set; }
    }

    public class SwitchUserAction
    {
        public string NewUser { get; set; }
    }
}
