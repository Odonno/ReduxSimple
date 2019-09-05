using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static ReduxSimple.Selectors;

namespace ReduxSimple.UnitTests.Setup.TodoListStore
{
    public static class Selectors
    {
        public static Func<TodoListState, ImmutableList<TodoItem>> SelectTodoList = state => state.TodoList;
        public static Func<TodoListState, string> SelectCurrentUser = state => state.CurrentUser;
        public static Func<TodoListState, string> SelectUselessProperty = state => state.UselessProperty;

        public static MemoizedSelectorWithProps<TodoListState, string, ImmutableList<TodoItem>, IEnumerable<TodoItem>> SelectSearchedItems = CreateSelector(
            SelectTodoList,
            (ImmutableList<TodoItem> items, string search) =>
            {
                return items.Where(item => item.Title.Contains(search));
            }
        );
    }
}
