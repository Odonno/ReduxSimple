```csharp
public sealed partial class TicTacToePage : Page
{
    public TicTacToePage()
    {
        InitializeComponent();

        // Get UI Elements
        var cellsGrids = CellsRootGrid.Children;

        // Observe changes on state
		Store.Select(
            CombineSelectors(SelectGameEnded, SelectWinner)
        )
            .UntilDestroyed(this)
            .Subscribe(x =>
            {
                var (gameEnded, winner) = x;

                YourTurnTextBlock.HideIf(gameEnded);
                StartNewGameButton.ShowIf(gameEnded);
                EndGameTextBlock.ShowIf(gameEnded);

                if (gameEnded)
                {
                    if (winner.HasValue)
                        EndGameTextBlock.Text = $"{winner.Value} won!";
                    else
                        EndGameTextBlock.Text = "It's a tie!";
                }
            });

        Store.Select(SelectCells)
            .UntilDestroyed(this)
            .Subscribe(cells =>
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    var cellGrid = cellsGrids[i] as Grid;
                    var textBlock = cellGrid.Children[0] as TextBlock;

                    if (cells[i].Mine.HasValue)
                        textBlock.Text = cells[i].Mine.Value ? "O" : "X";
                    else
                        textBlock.Text = string.Empty;
                }
            });

        // Observe UI events
        foreach (Grid cellGrid in cellsGrids)
        {
            cellGrid.Events().Tapped
                .Select(e =>
                {
                    var grid = e.Sender as Grid;
                    return new { Row = Grid.GetRow(grid), Column = Grid.GetColumn(grid) };
                })
                .Where(x =>
                {
                    var cell = Store.State.TicTacToe.Cells.First(c => c.Row == x.Row && c.Column == x.Column);
                    return !Store.State.TicTacToe.GameEnded && !cell.Mine.HasValue;
                })
                .Subscribe(x =>
                {
                    Store.Dispatch(new PlayAction { Row = x.Row, Column = x.Column });
                });
        }

        StartNewGameButton.Events().Click
            .Subscribe(_ => Store.Dispatch(new StartNewGameAction()));
    }
}
```