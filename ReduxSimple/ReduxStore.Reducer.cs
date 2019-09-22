using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace ReduxSimple
{
    /// <summary>
    /// The <see cref="ReduxStore{TState}" /> is a centralized object for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public sealed partial class ReduxStore<TState> where TState : class, new()
    {
        private readonly List<On<TState>> _reducers;

        /// <summary>
        /// Dynamically add a list of reducers to be used in the store.
        /// </summary>
        /// <param name="reducers">Reducers to add.</param>
        public void AddReducers(params On<TState>[] reducers)
        {
            _reducers.AddRange(reducers);
        }

        /// <summary>
        /// Reduces the specified state using the specified action and returns the new state. Does not mutate the current state of the store.
        /// </summary>
        /// <param name="state">The state to reduce.</param>
        /// <param name="action">The action to use for reducing the specified state.</param>
        /// <returns>The state that results from applying <paramref name="action"/> on <paramref name="state"/>.</returns>
        private TState Reduce(TState state, object action)
        {
            var actionName = action.GetType().FullName;
            var reducer = _reducers.FirstOrDefault(r => r.Types.Contains(actionName));

            if (reducer != null)
            {
                return reducer.Reduce(state, action);
            }
            return state;
        }

        /// <summary>
        /// Updates the state of the store to the specified state.
        /// </summary>
        /// <param name="state">The new state of the store.</param>
        private void UpdateState(TState state)
        {
            State = state;
            _stateSubject.OnNext(State);
        }
    }
}