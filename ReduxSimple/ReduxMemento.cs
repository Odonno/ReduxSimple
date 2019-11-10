using System;

namespace ReduxSimple
{
    public class ReduxMemento<TState> where TState : class, new()
    {
        public DateTime Date { get; set; }
        public TState PreviousState { get; }
        public object Action { get; }

        public ReduxMemento(DateTime date, TState state, object action)
        {
            Date = date;
            PreviousState = state;
            Action = action;
        }
    }
}
