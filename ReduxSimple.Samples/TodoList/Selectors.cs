using System.Collections.Immutable;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Samples.TodoList
{
    public static class Selectors
    {
        public static ISelectorWithoutProps<RootState, TodoListState> SelectTodoListState = CreateSelector(
            (RootState state) => state.TodoList
        );

        public static ISelectorWithoutProps<RootState, TodoFilter> SelectFilter = CreateSelector(
            SelectTodoListState,
            state => state.Filter
        );
        public static ISelectorWithoutProps<RootState, ImmutableList<TodoItem>> SelectItems = CreateSelector(
            SelectTodoListState,
            state => state.Items
        );
    }
}
