```csharp
public static class Effects
{
    public static Effect<TicTacToeState> TrackAction = CreateEffect<TicTacToeState>(
        () => Store.ObserveAction()
            .Do(action =>
            {
                TrackReduxAction(action);
            }),
        false
    );
}
```