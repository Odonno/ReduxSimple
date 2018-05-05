namespace ReduxSimple.Samples.TodoList
{
    public class SetFilterAction
    {
        public TodoFilter Filter { get; set; }
    }

    public class CreateTodoItemAction { }

    public class CompleteTodoItemAction
    {
        public int Id { get; set; }
    }

    public class RemoveTodoItemAction
    {
        public int Id { get; set; }
    }

    public class UpdateTodoItemAction
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
