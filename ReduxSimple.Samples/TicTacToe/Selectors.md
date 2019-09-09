```csharp
public static class Selectors
{
    public static Func<TicTacToeState, ImmutableArray<Cell>> SelectCells = state => state.Cells;
    public static Func<TicTacToeState, bool> SelectGameEnded = state => state.GameEnded;
    public static Func<TicTacToeState, Option<string>> SelectWinner = state => state.Winner;
}
```