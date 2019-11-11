using System.Collections.Immutable;

namespace ReduxSimple.Tests.Setup.TodoListStore
{
    public class TodoListState
    {
        public ImmutableList<TodoItem> TodoList { get; set; }
        public string CurrentUser { get; set; }
        public string UselessProperty { get; set; }
    }
}
