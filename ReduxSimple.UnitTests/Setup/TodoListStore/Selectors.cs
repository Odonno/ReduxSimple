using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static ReduxSimple.Selectors;

namespace ReduxSimple.UnitTests.Setup.TodoListStore
{
    public static class Selectors
    {
        public static ISelectorWithoutProps<TodoListState, ImmutableList<TodoItem>> SelectTodoList = CreateSelector(
            (TodoListState state) => state.TodoList
        );
        public static ISelectorWithoutProps<TodoListState, string> SelectCurrentUser = CreateSelector(
            (TodoListState state) => state.CurrentUser
        );
        public static ISelectorWithoutProps<TodoListState, string> SelectUselessProperty = CreateSelector(
            (TodoListState state) => state.UselessProperty
        );

        public static ISelectorWithProps<TodoListState, string, IEnumerable<TodoItem>> SelectSearchedItems = CreateSelector(
            SelectTodoList,
            (ImmutableList<TodoItem> items, string search) =>
            {
                return items.Where(item => item.Title.Contains(search));
            }
        );
    }
}
