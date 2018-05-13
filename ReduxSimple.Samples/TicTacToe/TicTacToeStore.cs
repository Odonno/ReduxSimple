using SuccincT.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static ReduxSimple.Samples.Common.EventTracking;

namespace ReduxSimple.Samples.TicTacToe
{
    public class TicTacToeStore : ReduxStoreWithHistory<TicTacToeState>
    {
        private static TicTacToeState DefaultState =>
            new TicTacToeState
            {
                Cells = Enumerable.Range(0, 9)
                    .Select(i => new Cell { Row = i / 3, Column = i % 3, Mine = Option<bool>.None() })
                    .ToImmutableArray(),
                Winner = Option<string>.None()
            };

        public TicTacToeStore() : base(DefaultState)
        {
        }

        protected override TicTacToeState Reduce(TicTacToeState state, object action)
        {
            TrackReduxAction(action);

            if (action is PlayAction playAction)
            {
                // Player take cell
                var cellsTurnPlayer = PlayerTakeCell(state.Cells, playAction);

                // Check end game
                var (gameEndedTurn1, winnerTurn1) = CheckEndGame(cellsTurnPlayer);

                if (gameEndedTurn1)
                {
                    return new TicTacToeState
                    {
                        Cells = cellsTurnPlayer,
                        GameEnded = gameEndedTurn1,
                        Winner = winnerTurn1
                    };
                }

                // Bot take cell
                var cellsTurnBot = BotTakeCell(cellsTurnPlayer);

                // Check end game
                var (gameEndedTurn2, winnerTurn2) = CheckEndGame(cellsTurnBot);

                return new TicTacToeState
                {
                    Cells = cellsTurnBot,
                    GameEnded = gameEndedTurn2,
                    Winner = winnerTurn2
                };
            }
            if (action is StartNewGameAction _)
            {
                return DefaultState;
            }
            return base.Reduce(state, action);
        }

        private static ImmutableArray<Cell> PlayerTakeCell(ImmutableArray<Cell> cells, PlayAction playAction)
        {
            var cellToUpdate = cells.Single(c => c.Row == playAction.Row && c.Column == playAction.Column);
            return cells.Replace(cellToUpdate, new Cell { Row = playAction.Row, Column = playAction.Column, Mine = true });
        }

        private ImmutableArray<Cell> BotTakeCell(ImmutableArray<Cell> cells)
        {
            var random = new Random();

            var availableCells = cells.Where(c => !c.Mine.HasValue);
            var cellToUpdate = availableCells.Skip(random.Next(availableCells.Count())).First();
            return cells.Replace(cellToUpdate, new Cell { Row = cellToUpdate.Row, Column = cellToUpdate.Column, Mine = false });
        }

        private (bool gameEnded, Option<string> winner) CheckEndGame(ImmutableArray<Cell> cells)
        {
            // Check rows
            var rowsGroups = cells.GroupBy(c => c.Row);
            foreach (var rowsGroup in rowsGroups)
            {
                if (HasPlayerWon(rowsGroup))
                    return (true, "You");
                if (HasBotWon(rowsGroup))
                    return (true, "Bot");
            }

            // Check columns
            var columnsGroups = cells.GroupBy(c => c.Column);
            foreach (var columnsGroup in columnsGroups)
            {
                if (HasPlayerWon(columnsGroup))
                    return (true, "You");
                if (HasBotWon(columnsGroup))
                    return (true, "Bot");
            }

            // Check diagonals
            var diagonalOne = cells.Where(c => c.Row == c.Column);
            if (HasPlayerWon(diagonalOne))
                return (true, "You");
            if (HasBotWon(diagonalOne))
                return (true, "Bot");

            var diagonalTwo = cells.Where(c => (2 - c.Row) == c.Column);
            if (HasPlayerWon(diagonalTwo))
                return (true, "You");
            if (HasBotWon(diagonalTwo))
                return (true, "Bot");

            return (cells.All(c => c.Mine.HasValue), Option<string>.None());
        }

        private static bool HasPlayerWon(IEnumerable<Cell> cells)
        {
            return cells.All(c => c.Mine.HasValue && c.Mine == true);
        }
        private static bool HasBotWon(IEnumerable<Cell> cells)
        {
            return cells.All(c => c.Mine.HasValue && c.Mine == false);
        }
    }
}
