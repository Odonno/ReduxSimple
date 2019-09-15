using System.Reactive.Linq;
using static ReduxSimple.Effects;
using static ReduxSimple.Samples.TicTacToe.TicTacToePage;
using static ReduxSimple.Samples.Common.EventTracking;

namespace ReduxSimple.Samples.TicTacToe
{
    public static class Effects
    {
        public static Effect<TicTacToeState> TrackAction = CreateEffect<TicTacToeState>(
            () => Store.ObserveAction()
                .Do(action =>
                {
                    TrackReduxAction(action);
                }),
            false
        );
    }
}
