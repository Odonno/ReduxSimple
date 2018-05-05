﻿using System;
using System.Linq;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ReduxSimple.Samples.TicTacToe
{
    public sealed partial class TicTacToePage : Page
    {
        private static TicTacToeStore _store = new TicTacToeStore();

        public TicTacToePage()
        {
            InitializeComponent();

            // Get UI Elements
            var cellsGrids = CellsRootGrid.Children;

            // Observe changes on state
            _store.ObserveState(state => new { state.GameEnded, state.Winner })
                .Subscribe(x =>
                {
                    YourTurnTextBlock.HideIf(x.GameEnded);
                    StartNewGameButton.ShowIf(x.GameEnded);
                    EndGameTextBlock.ShowIf(x.GameEnded);

                    if (x.GameEnded)
                    {
                        if (x.Winner.HasValue)
                            EndGameTextBlock.Text = $"{x.Winner.Value} won!";
                        else
                            EndGameTextBlock.Text = "It's a tie!";
                    }
                });

            _store.ObserveState(state => state.Cells)
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
            foreach (var cellGrid in cellsGrids)
            {
                Observable.FromEventPattern<TappedEventHandler, TappedRoutedEventArgs>(
                    h => cellGrid.Tapped += h,
                    h => cellGrid.Tapped -= h
                )
                    .Select(e =>
                    {
                        var grid = e.Sender as Grid;
                        return new { Row = Grid.GetRow(grid), Column = Grid.GetColumn(grid) };
                    })
                    .Where(x =>
                    {
                        var cell = _store.State.Cells.First(c => c.Row == x.Row && c.Column == x.Column);
                        return !_store.State.GameEnded && !cell.Mine.HasValue;
                    })
                    .Subscribe(x =>
                    {
                        _store.Dispatch(new PlayAction { Row = x.Row, Column = x.Column });
                    });
            }

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => StartNewGameButton.Click += h,
                h => StartNewGameButton.Click -= h
            )
                .Subscribe(e =>
                {
                    _store.Dispatch(new StartNewGameAction());
                });

            // Initialize UI
            YourTurnTextBlock.HideIf(_store.State.GameEnded);
            StartNewGameButton.ShowIf(_store.State.GameEnded);
            EndGameTextBlock.ShowIf(_store.State.GameEnded);

            if (_store.State.GameEnded)
            {
                if (_store.State.Winner.HasValue)
                    EndGameTextBlock.Text = $"{_store.State.Winner.Value} won!";
                else
                    EndGameTextBlock.Text = "It's a tie!";
            }

            for (int i = 0; i < _store.State.Cells.Length; i++)
            {
                var cellGrid = cellsGrids[i] as Grid;
                var textBlock = cellGrid.Children[0] as TextBlock;

                if (_store.State.Cells[i].Mine.HasValue)
                    textBlock.Text = _store.State.Cells[i].Mine.Value ? "O" : "X";
                else
                    textBlock.Text = string.Empty;
            }
        }
    }
}