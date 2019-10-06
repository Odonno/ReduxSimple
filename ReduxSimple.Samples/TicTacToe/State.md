```csharp
public class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public Option<bool> Mine { get; set; } = Option<bool>.None();
}

public class TicTacToeState
{
    public ImmutableArray<Cell> Cells { get; set; }
    public bool GameEnded { get; set; }
    public Option<string> Winner { get; set; }

    public static TicTacToeState InitialState =>
        new TicTacToeState
        {
            Cells = Enumerable.Range(0, 9)
                .Select(i => new Cell { Row = i / 3, Column = i % 3, Mine = Option<bool>.None() })
                .ToImmutableArray(),
            Winner = Option<string>.None()
        };

}
```