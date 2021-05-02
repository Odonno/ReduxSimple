using ReduxSimple.Entity;

namespace ReduxSimple.Uwp.Samples.TodoList
{
    public class TodoItemEntityState : EntityState<int, TodoItem>
    {
    }

    public static class Entities
    {
        public static EntityAdapter<int, TodoItem> TodoItemAdapter = EntityAdapter<int, TodoItem>.Create(item => item.Id);
    }
}
