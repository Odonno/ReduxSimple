using System;

namespace ReduxSimple
{
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult">Result of the selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TState, TSelectorResult, TResult>
    {
        /// <summary>
        /// Selector function
        /// </summary>
        public Func<TState, TSelectorResult> Selector { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult, TResult> ProjectorFunction { get; }

        public MemoizedSelector(
            Func<TState, TSelectorResult> selector,
            Func<TSelectorResult, TResult> projectorFunction
        )
        {
            Selector = selector;
            ProjectorFunction = projectorFunction;
        }

        public static MemoizedSelector<TState, TSelectorResult, TResult> Create<TMemoSelector1Result>(
            MemoizedSelector<TState, TMemoSelector1Result, TSelectorResult> selector,
            Func<TSelectorResult, TResult> projectorFunction
        )
        {
            return new MemoizedSelector<TState, TSelectorResult, TResult>(
                state => selector.ProjectorFunction(selector.Selector(state)),
                projectorFunction
            );
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TResult>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public Func<TState, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public Func<TState, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TResult> ProjectorFunction { get; }

        public MemoizedSelector(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TResult> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            ProjectorFunction = projectorFunction;
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TResult>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public Func<TState, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public Func<TState, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public Func<TState, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TResult> ProjectorFunction { get; }

        public MemoizedSelector(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TResult> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            ProjectorFunction = projectorFunction;
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TResult>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public Func<TState, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public Func<TState, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public Func<TState, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Fourth Selector function
        /// </summary>
        public Func<TState, TSelectorResult4> Selector4 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TResult> ProjectorFunction { get; }

        public MemoizedSelector(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TResult> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            Selector4 = selector4;
            ProjectorFunction = projectorFunction;
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
    /// <typeparam name="TSelectorResult5">Result of the fifth selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TResult>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public Func<TState, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public Func<TState, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public Func<TState, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Fourth Selector function
        /// </summary>
        public Func<TState, TSelectorResult4> Selector4 { get; }
        /// <summary>
        /// Fifth Selector function
        /// </summary>
        public Func<TState, TSelectorResult5> Selector5 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TResult> ProjectorFunction { get; }

        public MemoizedSelector(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TState, TSelectorResult5> selector5,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TResult> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            Selector4 = selector4;
            Selector5 = selector5;
            ProjectorFunction = projectorFunction;
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
    /// <typeparam name="TSelectorResult5">Result of the fifth selector</typeparam>
    /// <typeparam name="TSelectorResult6">Result of the sixth selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TResult>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public Func<TState, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public Func<TState, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public Func<TState, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Fourth Selector function
        /// </summary>
        public Func<TState, TSelectorResult4> Selector4 { get; }
        /// <summary>
        /// Fifth Selector function
        /// </summary>
        public Func<TState, TSelectorResult5> Selector5 { get; }
        /// <summary>
        /// Sixth Selector function
        /// </summary>
        public Func<TState, TSelectorResult6> Selector6 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TResult> ProjectorFunction { get; }

        public MemoizedSelector(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TState, TSelectorResult5> selector5,
            Func<TState, TSelectorResult6> selector6,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TResult> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            Selector4 = selector4;
            Selector5 = selector5;
            Selector6 = selector6;
            ProjectorFunction = projectorFunction;
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TState">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
    /// <typeparam name="TSelectorResult5">Result of the fifth selector</typeparam>
    /// <typeparam name="TSelectorResult6">Result of the sixth selector</typeparam>
    /// <typeparam name="TSelectorResult7">Result of the seventh selector</typeparam>
    /// <typeparam name="TResult">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TResult>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public Func<TState, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public Func<TState, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public Func<TState, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Fourth Selector function
        /// </summary>
        public Func<TState, TSelectorResult4> Selector4 { get; }
        /// <summary>
        /// Fifth Selector function
        /// </summary>
        public Func<TState, TSelectorResult5> Selector5 { get; }
        /// <summary>
        /// Sixth Selector function
        /// </summary>
        public Func<TState, TSelectorResult6> Selector6 { get; }
        /// <summary>
        /// Seventh Selector function
        /// </summary>
        public Func<TState, TSelectorResult7> Selector7 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TResult> ProjectorFunction { get; }

        public MemoizedSelector(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TState, TSelectorResult5> selector5,
            Func<TState, TSelectorResult6> selector6,
            Func<TState, TSelectorResult7> selector7,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TResult> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            Selector4 = selector4;
            Selector5 = selector5;
            Selector6 = selector6;
            Selector7 = selector7;
            ProjectorFunction = projectorFunction;
        }
    }
}
