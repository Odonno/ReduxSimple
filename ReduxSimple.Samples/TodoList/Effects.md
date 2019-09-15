```csharp
public static class Effects
{
    public static Effect<TodoListState> TrackAction = CreateEffect<TodoListState>(
        () => Store.ObserveAction()
            .Do(action =>
            {
                TrackReduxAction(action);
            }),
        false
    );
}
```