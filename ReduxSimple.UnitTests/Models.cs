using System.Collections.Immutable;
using static ReduxSimple.UnitTests.Functions;

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
            return ReduceTodoListState(state, action) ?? base.Reduce(state, action);
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

    public class HistoryStoreWithEmptyState : ReduxStoreWithHistory<EmptyState>
    {
    }

    public class TodoListStoreWithHistory : ReduxStoreWithHistory<TodoListState>
    {
        public TodoListStoreWithHistory(TodoListState initialState = null) : base(initialState)
        {
        }

        protected override TodoListState Reduce(TodoListState state, object action)
        {
            return ReduceTodoListState(state, action) ?? base.Reduce(state, action);
        }
    }
}
