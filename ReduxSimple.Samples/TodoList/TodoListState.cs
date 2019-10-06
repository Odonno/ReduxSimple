namespace ReduxSimple.Uwp.Samples.TodoList
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Completed { get; set; }
    }

    public enum TodoFilter
    {
        All,
        Todo,
        Completed
    }

    public class TodoListState
    {
        public TodoItemEntityState Items { get; set; }
        public TodoFilter Filter { get; set; }

        public static TodoListState InitialState =>
            new TodoListState
            {
                Filter = TodoFilter.All,
                Items = new TodoItemEntityState()
            };
    }
}
