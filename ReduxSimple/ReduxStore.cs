using System;
using System.Linq;
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
        private class ActionWithOrigin
        {
            public object Action { get; }
            public ActionOrigin Origin { get; set; }

            public ActionWithOrigin(object action, ActionOrigin origin)
            {
                Action = action;
                Origin = origin;
            }
        }

        private readonly TState _initialState;
        private readonly BehaviorSubject<TState> _stateSubject;
        private readonly Subject<ActionWithOrigin> _actionSubject = new Subject<ActionWithOrigin>();
        private readonly Subject<TState> _resetSubject = new Subject<TState>();
        private readonly FullStateComparer<TState> _fullStateComparer = new FullStateComparer<TState>();

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
            State = _initialState = initialState ?? new TState();
            _stateSubject = new BehaviorSubject<TState>(State);
        }

        /// <summary>
        /// Dispatches the specified action to the store, which reduces the current state of the store to a new state by performing the specified action
        /// on the current state.
        /// </summary>
        /// <param name="action">The action to be performed on the current state.</param>
        public void Dispatch(object action)
        {
            if (this is ReduxStoreWithHistory<TState> storeWithHistory)
            {
                storeWithHistory.Dispatch(action);
            }
            else
            {
                Dispatch(action, ActionOrigin.Normal);
            }
        }
        /// <summary>
        /// Dispatches the specified action to the store with the origin of the action (from current timeline, or previous one that meaning redone action)
        /// </summary>
        /// <param name="action">The action to be performed on the current state.</param>
        /// <param name="origin">The origin of the action.</param>
        internal void Dispatch(object action, ActionOrigin origin)
        {
            UpdateState(Reduce(State, action));
            _actionSubject.OnNext(new ActionWithOrigin(action, origin));
        }

        /// <summary>
        /// Reduces the specified state using the specified action and returns the new state. Does not mutate the current state of the store.
        /// Implementations should override this method to provide functionality specific to their use case.
        /// </summary>
        /// <param name="state">The state to reduce.</param>
        /// <param name="action">The action to use for reducing the specified state.</param>
        /// <returns>The state that results from applying <paramref name="action"/> on <paramref name="state"/>.</returns>
        protected virtual TState Reduce(TState state, object action)
        {
            return state;
        }

        #region Select methods

        /// <summary>
        /// Select the full state object.
        /// </summary>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about state changes.</returns>
        public IObservable<TState> Select()
        {
            return _stateSubject
                .DistinctUntilChanged(_fullStateComparer);
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial">The type of the partial state to be observed.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TPartial> Select<TPartial>(Func<TState, TPartial> selector)
        {
            return _stateSubject
                .Select(selector)
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial1">The return type of the first selector.</typeparam>
        /// <typeparam name="TPartial2">The return type of the second selector.</typeparam>
        /// <param name="selector1">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial1"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector2">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial2"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<Tuple<TPartial1, TPartial2>> Select<TPartial1, TPartial2>(Func<TState, TPartial1> selector1, Func<TState, TPartial2> selector2)
        {
            return Observable.CombineLatest(
                ApplySelector(selector1),
                ApplySelector(selector2),
                Tuple.Create
            );
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial1">The return type of the first selector.</typeparam>
        /// <typeparam name="TPartial2">The return type of the second selector.</typeparam>
        /// <typeparam name="TPartial3">The return type of the third selector.</typeparam>
        /// <param name="selector1">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial1"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector2">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial2"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector3">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial3"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<Tuple<TPartial1, TPartial2, TPartial3>> Select<TPartial1, TPartial2, TPartial3>(Func<TState, TPartial1> selector1, Func<TState, TPartial2> selector2, Func<TState, TPartial3> selector3)
        {
            return Observable.CombineLatest(
                ApplySelector(selector1),
                ApplySelector(selector2),
                ApplySelector(selector3),
                Tuple.Create
            );
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial1">The return type of the first selector.</typeparam>
        /// <typeparam name="TPartial2">The return type of the second selector.</typeparam>
        /// <typeparam name="TPartial3">The return type of the third selector.</typeparam>
        /// <typeparam name="TPartial4">The return type of the fourth selector.</typeparam>
        /// <param name="selector1">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial1"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector2">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial2"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector3">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial3"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector4">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial4"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<Tuple<TPartial1, TPartial2, TPartial3, TPartial4>> Select<TPartial1, TPartial2, TPartial3, TPartial4>(Func<TState, TPartial1> selector1, Func<TState, TPartial2> selector2, Func<TState, TPartial3> selector3, Func<TState, TPartial4> selector4)
        {
            return Observable.CombineLatest(
                ApplySelector(selector1),
                ApplySelector(selector2),
                ApplySelector(selector3),
                ApplySelector(selector4),
                Tuple.Create
            );
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial1">The return type of the first selector.</typeparam>
        /// <typeparam name="TPartial2">The return type of the second selector.</typeparam>
        /// <typeparam name="TPartial3">The return type of the third selector.</typeparam>
        /// <typeparam name="TPartial4">The return type of the fifth selector.</typeparam>
        /// <typeparam name="TPartial5">The return type of the fourth selector.</typeparam>
        /// <param name="selector1">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial1"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector2">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial2"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector3">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial3"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector4">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial4"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector5">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial5"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<Tuple<TPartial1, TPartial2, TPartial3, TPartial4, TPartial5>> Select<TPartial1, TPartial2, TPartial3, TPartial4, TPartial5>(Func<TState, TPartial1> selector1, Func<TState, TPartial2> selector2, Func<TState, TPartial3> selector3, Func<TState, TPartial4> selector4, Func<TState, TPartial5> selector5)
        {
            return Observable.CombineLatest(
                ApplySelector(selector1),
                ApplySelector(selector2),
                ApplySelector(selector3),
                ApplySelector(selector4),
                ApplySelector(selector5),
                Tuple.Create
            );
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial1">The return type of the first selector.</typeparam>
        /// <typeparam name="TPartial2">The return type of the second selector.</typeparam>
        /// <typeparam name="TPartial3">The return type of the third selector.</typeparam>
        /// <typeparam name="TPartial4">The return type of the fourth selector.</typeparam>
        /// <typeparam name="TPartial5">The return type of the fifth selector.</typeparam>
        /// <typeparam name="TPartial6">The return type of the sixth selector.</typeparam>
        /// <param name="selector1">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial1"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector2">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial2"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector3">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial3"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector4">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial4"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector5">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial5"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector6">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial6"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<Tuple<TPartial1, TPartial2, TPartial3, TPartial4, TPartial5, TPartial6>> Select<TPartial1, TPartial2, TPartial3, TPartial4, TPartial5, TPartial6>(Func<TState, TPartial1> selector1, Func<TState, TPartial2> selector2, Func<TState, TPartial3> selector3, Func<TState, TPartial4> selector4, Func<TState, TPartial5> selector5, Func<TState, TPartial6> selector6)
        {
            return Observable.CombineLatest(
                ApplySelector(selector1),
                ApplySelector(selector2),
                ApplySelector(selector3),
                ApplySelector(selector4),
                ApplySelector(selector5),
                ApplySelector(selector6),
                Tuple.Create
            );
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial1">The return type of the first selector.</typeparam>
        /// <typeparam name="TPartial2">The return type of the second selector.</typeparam>
        /// <typeparam name="TPartial3">The return type of the third selector.</typeparam>
        /// <typeparam name="TPartial4">The return type of the fourth selector.</typeparam>
        /// <typeparam name="TPartial5">The return type of the fifth selector.</typeparam>
        /// <typeparam name="TPartial6">The return type of the sixth selector.</typeparam>
        /// <typeparam name="TPartial7">The return type of the seventh selector.</typeparam>
        /// <param name="selector1">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial1"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector2">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial2"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector3">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial3"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector4">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial4"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector5">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial5"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector6">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial6"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="selector7">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial7"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<Tuple<TPartial1, TPartial2, TPartial3, TPartial4, TPartial5, TPartial6, TPartial7>> Select<TPartial1, TPartial2, TPartial3, TPartial4, TPartial5, TPartial6, TPartial7>(Func<TState, TPartial1> selector1, Func<TState, TPartial2> selector2, Func<TState, TPartial3> selector3, Func<TState, TPartial4> selector4, Func<TState, TPartial5> selector5, Func<TState, TPartial6> selector6, Func<TState, TPartial7> selector7)
        {
            return Observable.CombineLatest(
                ApplySelector(selector1),
                ApplySelector(selector2),
                ApplySelector(selector3),
                ApplySelector(selector4),
                ApplySelector(selector5),
                ApplySelector(selector6),
                ApplySelector(selector7),
                Tuple.Create
            );
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial">The return type of the selector.</typeparam>
        /// <typeparam name="TProps">The type of the props to use in the selector.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="props">
        /// The properties used in the selector.
        /// </param>
        /// <returns></returns>
        public IObservable<TPartial> Select<TPartial, TProps>(Func<TState, TProps, TPartial> selector, TProps props)
        {
            return _stateSubject
                .Select(state => selector(state, props))
                .DistinctUntilChanged();
        }

        #endregion

        #region ObserveState methods

        /// <summary>
        /// Observes the state of the store.
        /// </summary>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about state changes.</returns>
        [Obsolete("You should now use the Select method.")]
        public IObservable<TState> ObserveState()
        {
            return _stateSubject.DistinctUntilChanged(_fullStateComparer);
        }
        /// <summary>
        /// Observes a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TPartial">The type of the partial state to be observed.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TPartial"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        [Obsolete("You should now use the Select method.")]
        public IObservable<TPartial> ObserveState<TPartial>(Func<TState, TPartial> selector)
        {
            return _stateSubject.Select(selector).DistinctUntilChanged();
        }

        #endregion

        /// <summary>
        /// Observes actions being performed on the store.
        /// </summary>
        /// <param name="filter">Filter action by origin.</param>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about actions performed on the store.</returns>
        public IObservable<object> ObserveAction(ActionOriginFilter filter = ActionOriginFilter.All)
        {
            return _actionSubject
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
        public IObservable<T> ObserveAction<T>(ActionOriginFilter filter = ActionOriginFilter.All)
        {
            return _actionSubject
                .Where(x => filter.HasFlag((ActionOriginFilter)x.Origin))
                .Select(x => x.Action)
                .OfType<T>();
        }

        /// <summary>
        /// Resets the store to its initial state.
        /// </summary>
        public void Reset()
        {
            if (this is ReduxStoreWithHistory<TState> storeWithHistory)
            {
                storeWithHistory.Reset();
            }
            else
            {
                ResetState();
            }
        }

        /// <summary>
        /// Reset the state and trigger a new reset event.
        /// </summary>
        internal void ResetState()
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

        /// <summary>
        /// Updates the state of the store to the specified state.
        /// </summary>
        /// <param name="state">The new state of the store.</param>
        protected void UpdateState(TState state)
        {
            State = state;
            _stateSubject.OnNext(State);
        }

        /// <summary>
        /// Apply a selector to the state observable.
        /// </summary>
        /// <typeparam name="TPartial">The return type of the selector.</typeparam>
        /// <param name="selector">The selector to apply.</param>
        /// <returns>An <see cref="IObservable{TPartial}"/> that can be subscribed to in order to receive partial state changes based on the selector function.</returns>
        private IObservable<TPartial> ApplySelector<TPartial>(Func<TState, TPartial> selector)
        {
            return _stateSubject
                .Select(selector)
                .DistinctUntilChanged();
        }
    }
}