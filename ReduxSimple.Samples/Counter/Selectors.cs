using System;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Samples.Counter
{
    public static class Selectors
    {
        public static Func<RootState, CounterState> SelectCounterState = state => state.Counter;

        public static MemoizedSelector<RootState, CounterState, int> SelectCount = CreateSelector(
            SelectCounterState,
            state => state.Count
        );
    }
}
