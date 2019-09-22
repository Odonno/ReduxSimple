using SuccincT.Options;
using System;
using System.Collections.Immutable;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Samples.TicTacToe
{
    public static class Selectors
    {
        public static Func<RootState, TicTacToeState> SelectTicTacToeState = state => state.TicTacToe;

        public static MemoizedSelector<RootState, TicTacToeState, ImmutableArray<Cell>> SelectCells = CreateSelector(
            SelectTicTacToeState,
            state => state.Cells
        );
        public static MemoizedSelector<RootState, TicTacToeState, bool> SelectGameEnded = CreateSelector(
            SelectTicTacToeState,
            state => state.GameEnded
        );
        public static MemoizedSelector<RootState, TicTacToeState, Option<string>> SelectWinner = CreateSelector(
            SelectTicTacToeState,
            state => state.Winner
        );
    }
}
