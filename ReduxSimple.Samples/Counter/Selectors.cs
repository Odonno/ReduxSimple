using static ReduxSimple.Selectors;

namespace ReduxSimple.Samples.Counter
{
    public static class Selectors
    {
        public static ISelectorWithoutProps<RootState, CounterState> SelectCounterState = CreateSelector(
            (RootState state) => state.Counter
        );

        public static ISelectorWithoutProps<RootState, int> SelectCount = CreateSelector(
            SelectCounterState,
            state => state.Count
        );
    }
}
