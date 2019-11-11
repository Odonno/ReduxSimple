using System.Collections.Immutable;

namespace ReduxSimple.Tests.Setup.TodoListStore
{
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
        public string UselessProperty { get; set; }
    }
}
