using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReduxSimple
{
    /// <summary>
    /// The <see cref="ReduxStore{TState}" /> is a centralized object for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public sealed partial class ReduxStore<TState> where TState : class, new()
    {
        private readonly Subject<ActionDispatched> _toDispatchSubject = new Subject<ActionDispatched>();
        private readonly Subject<ActionDispatchedWithOrigin> _dispatchedSubject = new Subject<ActionDispatchedWithOrigin>();

        private readonly IConnectableObservable<ActionDispatchedWithOrigin> _dispatchedAction;

        /// <summary>
        /// Dispatches the specified action to the store, which reduces the current state of the store to a new state by performing the specified action
        /// on the current state.
        /// </summary>
        /// <param name="action">The action to be performed on the current state.</param>
        public void Dispatch(object action)
        {
            if (action == null)
            {
                Debug.WriteLine("[Warning] Dispatching a `null` action is forbidden.");
                return;
            }

            if (TimeTravelEnabled)
            {
                _toDispatchSubject.OnNext(new ActionDispatchedWithRewriteHistory(action, true));
            }
            else
            {
                _toDispatchSubject.OnNext(new ActionDispatchedWithOrigin(action, ActionOrigin.Normal));
            }
        }

        /// <summary>
        /// Dispatches the specified action to the store with the origin of the action (from current timeline, or previous one that meaning redone action)
        /// </summary>
        /// <param name="action">The action to be performed on the current state.</param>
        /// <param name="origin">The origin of the action.</param>
        private void ExecuteDispatch(ActionDispatchedWithOrigin actionDispatchedWithOrigin)
        {
            UpdateState(Reduce(State, actionDispatchedWithOrigin.Action));
            _dispatchedSubject.OnNext(actionDispatchedWithOrigin);
        }
        /// <summary>
        /// Dispatches the specified action to the store by specifying that it should clear the list of future actions (in time-travel scenario)
        /// </summary>
        /// <param name="action">The action to be performed on the current state.</param>
        /// <param name="rewriteHistory">Clear the list of future actions (that were already fired before in the store).</param>
        private void ExecuteDispatch(ActionDispatchedWithRewriteHistory actionWithRewriteHistory)
        {
            if (actionWithRewriteHistory.RewriteHistory)
            {
                _futureActions.Clear();
            }

            _pastMementos.Push(new ReduxMemento<TState>(DateTime.Now, State, actionWithRewriteHistory.Action));

            ExecuteDispatch(
                new ActionDispatchedWithOrigin(
                    actionWithRewriteHistory.Action,
                    actionWithRewriteHistory.RewriteHistory ? ActionOrigin.Normal : ActionOrigin.Redone
                )
            );
        }

        /// <summary>
        /// Observes actions being performed on the store.
        /// </summary>
        /// <param name="filter">Filter action by origin.</param>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about actions performed on the store.</returns>
        public IObservable<object> ObserveAction(ActionOriginFilter filter = ActionOriginFilter.Normal)
        {
            return _dispatchedAction
                .Where(x => filter.HasFlag((ActionOriginFilter)x.Origin))
                .Select(x => x.Action);
        }
        /// <summary>
        /// Observes actions of a specific type being performed on the store.
        /// </summary>
        /// <typeparam name="T">The type of actions that the subscriber is interested in.</typeparam>
        /// <param name="filter">Filter action by origin.</param>
        /// <returns>
        /// An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates whenever an action of <typeparamref name="T"/> is performed on the store.
        /// </returns>
        public IObservable<T> ObserveAction<T>(ActionOriginFilter filter = ActionOriginFilter.Normal)
        {
            return _dispatchedAction
                .Where(x => filter.HasFlag((ActionOriginFilter)x.Origin))
                .Select(x => x.Action)
                .OfType<T>();
        }
    }
}