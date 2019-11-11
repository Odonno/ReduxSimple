using System.Reactive.Linq;
using static ReduxSimple.Effects;
using static ReduxSimple.Uwp.Samples.App;
using static ReduxSimple.Uwp.Samples.Common.EventTracking;

namespace ReduxSimple.Uwp.Samples
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
