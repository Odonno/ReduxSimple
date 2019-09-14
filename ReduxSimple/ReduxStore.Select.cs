using System;
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
        private readonly FullStateComparer<TState> _fullStateComparer = new FullStateComparer<TState>();

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