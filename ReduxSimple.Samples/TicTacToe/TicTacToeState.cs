using SuccincT.Options;
using System.Collections.Immutable;

namespace ReduxSimple.Samples.TicTacToe
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Option<bool> Mine { get; set; }
    }

    public class TicTacToeState
    {
        public ImmutableArray<Cell> Cells { get; set; }
        public bool GameEnded { get; set; }
        public Option<string> Winner { get; set; }
    }
}
