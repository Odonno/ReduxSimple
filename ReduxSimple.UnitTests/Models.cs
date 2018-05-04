using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            if (obj is TodoListState state)
            {
                return TodoList.Equals(state.TodoList) &&
                    CurrentUser == state.CurrentUser;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            var hashCode = -1396220578;
            hashCode = hashCode * -1521134295 + EqualityComparer<ImmutableList<TodoItem>>.Default.GetHashCode(TodoList);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CurrentUser);
            return hashCode;
        }
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

    public class HistoryStoreWithEmptyState : ReduxStore<EmptyState>
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
