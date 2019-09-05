using System;

namespace ReduxSimple
{
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function (with props)
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TProps">Properties used in selectors</typeparam>
    /// <typeparam name="TSelectorResult">Result of the selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelectorWithProps<TState, TProps, TSelectorResult, TResult>
    {
        /// <summary>
        /// Selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult> Selector { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult, TProps, TResult> ProjectorFunction { get; }

        public MemoizedSelectorWithProps(
            Func<TState, TProps, TSelectorResult> selector,
            Func<TSelectorResult, TProps, TResult> projectorFunction
        )
        {
            Selector = selector;
            ProjectorFunction = projectorFunction;
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function (with props)
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TProps">Properties used in selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TResult>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TProps, TResult> ProjectorFunction { get; }

        public MemoizedSelectorWithProps(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TProps, TResult> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            ProjectorFunction = projectorFunction;
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function (with props)
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TProps">Properties used in selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TResult>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TResult> ProjectorFunction { get; }

        public MemoizedSelectorWithProps(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TResult> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            ProjectorFunction = projectorFunction;
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function (with props)
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TProps">Properties used in selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TResult>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Fourth Selector function
        /// </summary>
        public Func<TState, TProps, TSelectorResult4> Selector4 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TResult> ProjectorFunction { get; }

        public MemoizedSelectorWithProps(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TResult> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            Selector4 = selector4;
            ProjectorFunction = projectorFunction;
        }
    }
}
