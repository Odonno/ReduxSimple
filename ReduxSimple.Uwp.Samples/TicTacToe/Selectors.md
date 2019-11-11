```csharp
public static class Selectors
{
    public static ISelectorWithoutProps<RootState, TicTacToeState> SelectTicTacToeState = CreateSelector(
        (RootState state) => state.TicTacToe
    );

    public static ISelectorWithoutProps<RootState, ImmutableArray<Cell>> SelectCells = CreateSelector(
        SelectTicTacToeState,
        state => state.Cells
    );
    public static ISelectorWithoutProps<RootState, bool> SelectGameEnded = CreateSelector(
        SelectTicTacToeState,
        state => state.GameEnded
    );
    public static ISelectorWithoutProps<RootState, Option<string>> SelectWinner = CreateSelector(
        SelectTicTacToeState,
        state => state.Winner
    );
}
```