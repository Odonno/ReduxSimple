using System.Reactive.Linq;
using static ReduxSimple.Effects;
using static ReduxSimple.Samples.App;
using static ReduxSimple.Samples.Common.EventTracking;

namespace ReduxSimple.Samples
{
    public static class Effects
    {
        public static Effect<RootState> TrackAction = CreateEffect<RootState>(
            () => Store.ObserveAction()
                .Do(action =>
                {
                    TrackReduxAction(action);
                }),
            false
        );
    }
}
