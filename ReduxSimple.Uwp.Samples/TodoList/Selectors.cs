using ReduxSimple.Entity;
using System.Collections.Generic;
using static ReduxSimple.Selectors;
using static ReduxSimple.Uwp.Samples.TodoList.Entities;

namespace ReduxSimple.Uwp.Samples.TodoList
{
    public static class Selectors
    {
        public static ISelectorWithoutProps<RootState, TodoListState> SelectTodoListState = CreateSelector(
            (RootState state) => state.TodoList
        );

        private static readonly ISelectorWithoutProps<RootState, TodoItemEntityState> SelectItemsEntityState = CreateSelector(
            SelectTodoListState,
            state => state.Items
        );
        private static readonly EntitySelectors<RootState, TodoItem, int> TodoItemSelectors = TodoItemAdapter.GetSelectors(SelectItemsEntityState);

        public static ISelectorWithoutProps<RootState, TodoFilter> SelectFilter = CreateSelector(
            SelectTodoListState,
            state => state.Filter
        );
        public static ISelectorWithoutProps<RootState, List<TodoItem>> SelectItems = TodoItemSelectors.SelectEntities;
    }
}
