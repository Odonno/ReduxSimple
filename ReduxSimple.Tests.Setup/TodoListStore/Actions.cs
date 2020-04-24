namespace ReduxSimple.Tests.Setup.TodoListStore
{
    public class AddTodoItemAction
    {
        public TodoItem? TodoItem { get; set; }
    }

    public class SwitchUserAction
    {
        public string? NewUser { get; set; }
    }

    public class ResetStateAction { }
}
