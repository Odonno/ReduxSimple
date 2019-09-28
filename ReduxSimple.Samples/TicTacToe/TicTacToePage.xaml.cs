using Microsoft.Toolkit.Uwp.UI.Animations;
using ReduxSimple.Uwp.Samples.Extensions;
using System;
using System.Linq;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using static ReduxSimple.Uwp.Samples.App;
using static ReduxSimple.Uwp.Samples.TicTacToe.Selectors;

namespace ReduxSimple.Uwp.Samples.TicTacToe
{
    public sealed partial class TicTacToePage : Page
    {
        public TicTacToePage()
        {
            InitializeComponent();

            // Get UI Elements
            var cellsGrids = CellsRootGrid.Children;

            // Observe changes on state
            Observable.CombineLatest(
                Store.Select(SelectGameEnded),
                Store.Select(SelectWinner),
                Tuple.Create
            )
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
                        var grid = e.OriginalSource as Grid;
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

            // Initialize Components
            HistoryComponent.Initialize(Store);

            // Initialize Documentation
            DocumentationComponent.LoadMarkdownFilesAsync("TicTacToe");

            ContentGrid.Events().Tapped
                .Subscribe(_ => DocumentationComponent.Collapse());
            DocumentationComponent.ObserveOnExpanded()
                .Subscribe(_ => ContentGrid.Blur(5).Start());
            DocumentationComponent.ObserveOnCollapsed()
                .Subscribe(_ => ContentGrid.Blur(0).Start());
        }
    }
}
