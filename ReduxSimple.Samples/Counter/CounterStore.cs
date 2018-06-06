using static ReduxSimple.Samples.Common.EventTracking;

namespace ReduxSimple.Samples.Counter
{
    public class CounterStore : ReduxStoreWithHistory<CounterState>
    {
        protected override CounterState Reduce(CounterState state, object action)
        {
            TrackReduxAction(action);

            if (action is IncrementAction _)
            {
                return new CounterState { Count = state.Count + 1 };
            }
            if (action is DecrementAction _)
            {
                return new CounterState { Count = state.Count - 1 };
            }
            return base.Reduce(state, action);
        }
    }
}
