using System.Collections.Generic;

namespace ReduxSimple
{
    public class ReduxHistory<TState> where TState : class, new()
    {
        public List<ReduxMemento<TState>> PreviousStates { get; }
        public List<object> FutureActions { get; }

        public ReduxHistory(List<ReduxMemento<TState>> previousStates, List<object> futureActions)
        {
            PreviousStates = previousStates;
            FutureActions = futureActions;
        }
    }
}
