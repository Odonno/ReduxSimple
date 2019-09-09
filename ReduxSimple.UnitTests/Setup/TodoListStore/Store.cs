using static ReduxSimple.UnitTests.Setup.TodoListStore.Functions;

namespace ReduxSimple.UnitTests.Setup.TodoListStore
{
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
