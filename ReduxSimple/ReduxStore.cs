using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReduxSimple
{
    /// <summary>
    /// The base class for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public class ReduxStore<TState> where TState : class, new()
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

        private readonly IEnumerable<On<TState>> _reducers;
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
        /// <param name="reducers">A list of reducers to update state when an action is triggered.</param>
        /// <param name="initialState">The initial state to put the store in; if <c>null</c>, a default value is constructed using <c>new TState()</c>.</param>
        public ReduxStore(
            IEnumerable<On<TState>> reducers,
            TState initialState = null
        )
        {
            _reducers = reducers;
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
            if (action == null)
            {
                Debug.WriteLine("[Warning] Dispatching a `null` action is forbidden.");
            }

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
        /// <typeparam name="TSelectorResult">The type of the selector result.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TSelectorResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TSelectorResult> Select<TSelectorResult>(Func<TState, TSelectorResult> selector)
        {
            return _stateSubject
                .Select(selector)
                .DistinctUntilChanged();
        }
        
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TSelectorResult">The return type of the selector.</typeparam>
        /// <typeparam name="TProps">The type of the props to use in the selector.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TSelectorResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="props">
        /// The properties used in the selector.
        /// </param>
        /// <returns></returns>
        public IObservable<TSelectorResult> Select<TSelectorResult, TProps>(Func<TState, TProps, TSelectorResult> selector, TProps props)
        {
            return _stateSubject
                .Select(state => selector(state, props))
                .DistinctUntilChanged();
        }

        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TSelectorResult">The type of the selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TSelectorResult, TResult>(MemoizedSelector<TState, TSelectorResult, TResult> memo)
        {
            return ApplySelector(memo.Selector)
                .Select(memo.ProjectorFunction)
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TSelectorResult1">The type of the first selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult2">The type of the second selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TSelectorResult1, TSelectorResult2, TResult>(MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TResult> memo)
        {
            return Observable.CombineLatest(
                ApplySelector(memo.Selector1),
                ApplySelector(memo.Selector2),
                Tuple.Create
            )
                .Select(x =>
                    memo.ProjectorFunction(
                        x.Item1,
                        x.Item2
                    )
                )
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TSelectorResult1">The type of the first selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult2">The type of the second selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult3">The type of the third selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TSelectorResult1, TSelectorResult2, TSelectorResult3, TResult>(MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TResult> memo)
        {
            return Observable.CombineLatest(
                ApplySelector(memo.Selector1),
                ApplySelector(memo.Selector2),
                ApplySelector(memo.Selector3),
                Tuple.Create
            )
                .Select(x =>
                    memo.ProjectorFunction(
                        x.Item1,
                        x.Item2,
                        x.Item3
                    )
                )
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TSelectorResult1">The type of the first selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult2">The type of the second selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult3">The type of the third selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult4">The type of the fourth selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TResult>(MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TResult> memo)
        {
            return Observable.CombineLatest(
                ApplySelector(memo.Selector1),
                ApplySelector(memo.Selector2),
                ApplySelector(memo.Selector3),
                ApplySelector(memo.Selector4),
                Tuple.Create
            )
                .Select(x =>
                    memo.ProjectorFunction(
                        x.Item1,
                        x.Item2,
                        x.Item3,
                        x.Item4
                    )
                )
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TSelectorResult1">The type of the first selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult2">The type of the second selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult3">The type of the third selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult4">The type of the fourth selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult5">The type of the fifth selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TResult>(MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TResult> memo)
        {
            return Observable.CombineLatest(
                ApplySelector(memo.Selector1),
                ApplySelector(memo.Selector2),
                ApplySelector(memo.Selector3),
                ApplySelector(memo.Selector4),
                ApplySelector(memo.Selector5),
                Tuple.Create
            )
                .Select(x =>
                    memo.ProjectorFunction(
                        x.Item1,
                        x.Item2,
                        x.Item3,
                        x.Item4,
                        x.Item5
                    )
                )
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TSelectorResult1">The type of the first selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult2">The type of the second selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult3">The type of the third selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult4">The type of the fourth selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult5">The type of the fifth selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult6">The type of the sixth selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TResult>(MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TResult> memo)
        {
            return Observable.CombineLatest(
                ApplySelector(memo.Selector1),
                ApplySelector(memo.Selector2),
                ApplySelector(memo.Selector3),
                ApplySelector(memo.Selector4),
                ApplySelector(memo.Selector5),
                ApplySelector(memo.Selector6),
                Tuple.Create
            )
                .Select(x =>
                    memo.ProjectorFunction(
                        x.Item1,
                        x.Item2,
                        x.Item3,
                        x.Item4,
                        x.Item5,
                        x.Item6
                    )
                )
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TSelectorResult1">The type of the first selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult2">The type of the second selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult3">The type of the third selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult4">The type of the fourth selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult5">The type of the fifth selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult6">The type of the sixth selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult7">The type of the seventh selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TResult>(MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TResult> memo)
        {
            return Observable.CombineLatest(
                ApplySelector(memo.Selector1),
                ApplySelector(memo.Selector2),
                ApplySelector(memo.Selector3),
                ApplySelector(memo.Selector4),
                ApplySelector(memo.Selector5),
                ApplySelector(memo.Selector6),
                ApplySelector(memo.Selector7),
                Tuple.Create
            )
                .Select(x =>
                    memo.ProjectorFunction(
                        x.Item1,
                        x.Item2,
                        x.Item3,
                        x.Item4,
                        x.Item5,
                        x.Item6,
                        x.Item7
                    )
                )
                .DistinctUntilChanged();
        }

        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TProps">The type of the props to use in the selector.</typeparam>
        /// <typeparam name="TSelectorResult">The type of the selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="props">
        /// The properties used in the selector.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TProps, TSelectorResult, TResult>(
            MemoizedSelectorWithProps<TState, TProps, TSelectorResult, TResult> memo,
            TProps props
        )
        {
            return ApplySelectorWithProps(memo.Selector, props)
                 .Select(result => memo.ProjectorFunction(result, props))
                 .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TProps">The type of the props to use in the selector.</typeparam>
        /// <typeparam name="TSelectorResult1">The type of the first selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult2">The type of the second selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="props">
        /// The properties used in the selector.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TProps, TSelectorResult1, TSelectorResult2, TResult>(
            MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TResult> memo,
            TProps props
        )
        {
            return Observable.CombineLatest(
                ApplySelectorWithProps(memo.Selector1, props),
                ApplySelectorWithProps(memo.Selector2, props),
                Tuple.Create
            )
                .Select(x =>
                    memo.ProjectorFunction(
                        x.Item1,
                        x.Item2,
                        props
                    )
                )
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TProps">The type of the props to use in the selector.</typeparam>
        /// <typeparam name="TSelectorResult1">The type of the first selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult2">The type of the second selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult3">The type of the third selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="props">
        /// The properties used in the selector.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TResult>(
            MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TResult> memo,
            TProps props
        )
        {
            return Observable.CombineLatest(
                ApplySelectorWithProps(memo.Selector1, props),
                ApplySelectorWithProps(memo.Selector2, props),
                ApplySelectorWithProps(memo.Selector3, props),
                Tuple.Create
            )
                .Select(x =>
                    memo.ProjectorFunction(
                        x.Item1,
                        x.Item2,
                        x.Item3,
                        props
                    )
                )
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TProps">The type of the props to use in the selector.</typeparam>
        /// <typeparam name="TSelectorResult1">The type of the first selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult2">The type of the second selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult3">The type of the third selector's memoized selector result.</typeparam>
        /// <typeparam name="TSelectorResult4">The type of the fourth selector's memoized selector result.</typeparam>
        /// <typeparam name="TResult">The type of the projector's memoized selector result.</typeparam>
        /// <param name="memo">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="props">
        /// The properties used in the selector.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TResult>(
            MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TResult> memo,
            TProps props
        )
        {
            return Observable.CombineLatest(
                ApplySelectorWithProps(memo.Selector1, props),
                ApplySelectorWithProps(memo.Selector2, props),
                ApplySelectorWithProps(memo.Selector3, props),
                ApplySelectorWithProps(memo.Selector4, props),
                Tuple.Create
            )
                .Select(x =>
                    memo.ProjectorFunction(
                        x.Item1,
                        x.Item2,
                        x.Item3,
                        x.Item4,
                        props
                    )
                )
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
        /// <typeparam name="TSelectorResult">The type of the partial state to be observed.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TSelectorResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        [Obsolete("You should now use the Select method.")]
        public IObservable<TSelectorResult> ObserveState<TSelectorResult>(Func<TState, TSelectorResult> selector)
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
        /// <typeparam name="TSelectorResult">The return type of the selector.</typeparam>
        /// <param name="selector">The selector to apply.</param>
        /// <returns>An <see cref="IObservable{TSelectorResult}"/> that can be subscribed to in order to receive partial state changes based on the selector function.</returns>
        private IObservable<TSelectorResult> ApplySelector<TSelectorResult>(Func<TState, TSelectorResult> selector)
        {
            return _stateSubject
                .Select(selector)
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Apply a selector with props to the state observable.
        /// </summary>
        /// <typeparam name="TProps">The type of the props used in the selector.</typeparam>
        /// <typeparam name="TSelectorResult">The return type of the selector.</typeparam>
        /// <param name="selector">The selector to apply.</param>
        /// <param name="props">The props to use in the selector.</param>
        /// <returns>An <see cref="IObservable{TSelectorResult}"/> that can be subscribed to in order to receive partial state changes based on the selector function.</returns>
        private IObservable<TSelectorResult> ApplySelectorWithProps<TProps, TSelectorResult>(Func<TState, TProps, TSelectorResult> selector, TProps props)
        {
            return _stateSubject
                .Select(state => selector(state, props))
                .DistinctUntilChanged();
        }
    }
}