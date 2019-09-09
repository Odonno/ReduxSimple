using System;

namespace ReduxSimple.Samples.Counter
{
    public static class Selectors
    {
        public static Func<CounterState, int> SelectCount = state => state.Count;
    }
}
