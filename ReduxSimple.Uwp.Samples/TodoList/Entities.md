```csharp
public class TodoItemEntityState : EntityState<TodoItem, int>
{
}

public static class Entities
{
    public static EntityAdapter<TodoItem, int> TodoItemAdapter = EntityAdapter<TodoItem, int>.Create(item => item.Id);
}
```