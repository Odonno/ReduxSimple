using System;
using System.Reactive.Subjects;

namespace ReduxSimple
{
    /// <summary>
    /// The <see cref="ReduxStore{TState}" /> is a centralized object for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public sealed partial class ReduxStore<TState> where TState : class, new()
    {
        private readonly Subject<TState> _resetSubject = new Subject<TState>();

        /// <summary>
        /// Resets the store to its initial state.
        /// </summary>
        public void Reset()
        {
            if (TimeTravelEnabled)
            {
                _pastMementos.Clear();
                _futureActions.Clear();
            }

            ResetState();
        }

        /// <summary>
        /// Reset the state and trigger a new reset event.
        /// </summary>
        private void ResetState()
        {
            UpdateState(_initialState);
            _resetSubject.OnNext(State);
        }

        /// <summary>
        /// Observes the reset operation being performed on the store.
        /// </summary>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates whenever the store is reset to its initial state.</returns>
        public IObservable<TState> ObserveReset()
        {
            return _resetSubject;
        }
    }
}