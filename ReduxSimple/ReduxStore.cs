using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReduxSimple
{
    /// <summary>
    /// The base class for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public abstract class ReduxStore<TState> where TState : class, new()
    {
        private readonly Subject<TState> _stateSubject = new Subject<TState>();
        private readonly Subject<object> _actionSubject = new Subject<object>();

        /// <summary>
        /// Gets the current state of the store.
        /// </summary>
        public TState State { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReduxStore{TState}"/> class.
        /// </summary>
        /// <param name="initialState">The initial state to put the store in; if <c>null</c>, a default value is constructed using <c>new TState()</c>.</param>
        protected ReduxStore(TState initialState = null)
        {
            State = initialState ?? new TState();
        }

        /// <summary>
        /// Dispatches the specified action to the store, which reduces the current state of the store to a new state by performing the specified action
        /// on the current state.
        /// </summary>
        /// <param name="action">The action to be performed on the current state.</param>
        public virtual void Dispatch(object action)
        {
            UpdateState(Reduce(State, action));
            _actionSubject.OnNext(action);
        }

        /// <summary>
        /// Reduces the specified state using the specified action and returns the new state. Does not mutate the current state of the store.
        /// Implementations should override this method to provide functionality specific to their use case.
        /// </summary>
        /// <param name="state">The state to reduce.</param>
        /// <param name="action">The action to use for reducing the specified state.</param>
        /// <returns>The state that results from applying <paramref name="action"/> on <paramref name="state"/>.</returns>
        public virtual TState Reduce(TState state, object action)
        {
            return state;
        }

        /// <summary>
        /// Observes the state of the store.
        /// </summary>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about state changes.</returns>
        public IObservable<TState> ObserveState()
        {
            return _stateSubject.AsObservable().DistinctUntilChanged();
        }
        /// <summary>
        /// Observes a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial">The type of the partial state to be observed.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TPartial> ObserveState<TPartial>(Func<TState, TPartial> selector)
        {
            return _stateSubject.Select(selector).DistinctUntilChanged();
        }

        /// <summary>
        /// Observes actions being performed on the store.
        /// </summary>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about actions performed on the store.</returns>
        public IObservable<object> ObserveAction()
        {
            return _actionSubject.AsObservable();
        }
        /// <summary>
        /// Observes actions of a specific type being performed on the store.
        /// </summary>
        /// <typeparam name="T">The type of actions that the subscriber is interested in.</typeparam>
        /// <returns>
        /// An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates whenever an action of <typeparamref name="T"/> is performed on the store.
        /// </returns>
        public IObservable<T> ObserveAction<T>() where T : class
        {
            return _actionSubject.OfType<T>().AsObservable();
        }

        /// <summary>
        /// Updates the state of the store to the specified state.
        /// </summary>
        /// <param name="state">The new state of the store.</param>
        protected void UpdateState(TState state)
        {
            State = state;
            _stateSubject.OnNext(State);
        }
    }
}