using SuccincT.Options;
using System.Collections.Immutable;

namespace ReduxSimple.Uwp.Samples.TicTacToe
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Option<bool> Mine { get; set; } = Option<bool>.None();
    }

    public class TicTacToeState
    {
        public ImmutableArray<Cell> Cells { get; set; } = ImmutableArray<Cell>.Empty;
        public bool GameEnded { get; set; }
        public Option<string> Winner { get; set; } = Option<string>.None();
    }
}
