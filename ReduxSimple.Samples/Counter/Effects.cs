using System.Reactive.Linq;
using static ReduxSimple.Effects;
using static ReduxSimple.Samples.Counter.CounterPage;
using static ReduxSimple.Samples.Common.EventTracking;

namespace ReduxSimple.Samples.Counter
{
    public static class Effects
    {
        public static Effect<CounterState> TrackAction = CreateEffect<CounterState>(
            () => Store.ObserveAction()
                .Do(action =>
                {
                    TrackReduxAction(action);
                }),
            false
        );
    }
}
