using System;
using System.Collections.Immutable;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Samples.TodoList
{
    public static class Selectors
    {
        public static Func<RootState, TodoListState> SelectTodoListState = state => state.TodoList;

        public static MemoizedSelector<RootState, TodoListState, TodoFilter> SelectFilter = CreateSelector(
            SelectTodoListState,
            state => state.Filter
        );
        public static MemoizedSelector<RootState, TodoListState, ImmutableList<TodoItem>> SelectItems = CreateSelector(
            SelectTodoListState,
            state => state.Items
        );
    }
}
