```csharp
public static class Effects
{
    public static Effect<CounterState> TrackAction = CreateEffect<CounterState>(
        () => Store.ObserveAction()
            .Do(action =>
            {
                TrackReduxAction(action);
            }),
        false
    );
}
```