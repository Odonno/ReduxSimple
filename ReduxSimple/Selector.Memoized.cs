using System;
using System.Reactive.Linq;

namespace ReduxSimple
{
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function.
    /// </summary>
    /// <typeparam name="TInput">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult">Result of the selector</typeparam>
    /// <typeparam name="TOutput">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TInput, TSelectorResult, TOutput> : ISelectorWithoutProps<TInput, TOutput>
    {
        /// <summary>
        /// Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult> Selector { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult, TOutput> ProjectorFunction { get; }

        public MemoizedSelector(
            ISelectorWithoutProps<TInput, TSelectorResult> selector,
            Func<TSelectorResult, TOutput> projectorFunction
        )
        {
            Selector = selector;
            ProjectorFunction = projectorFunction;
        }

        public TOutput Apply(TInput input)
        {
            var selectorResult = Selector.Apply(input);
            return ProjectorFunction(selectorResult);
        }
        public IObservable<TOutput> Apply(IObservable<TInput> input)
        {
            return input
                .Select(Apply)
                .DistinctUntilChanged();
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TInput">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TOutput">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TInput, TSelectorResult1, TSelectorResult2, TOutput> : ISelectorWithoutProps<TInput, TOutput>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TOutput> ProjectorFunction { get; }

        public MemoizedSelector(
            ISelectorWithoutProps<TInput, TSelectorResult1> selector1,
            ISelectorWithoutProps<TInput, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TOutput> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            ProjectorFunction = projectorFunction;
        }

        public TOutput Apply(TInput input)
        {
            var selector1Result = Selector1.Apply(input);
            var selector2Result = Selector2.Apply(input);

            return ProjectorFunction(
                selector1Result,
                selector2Result
            );
        }
        public IObservable<TOutput> Apply(IObservable<TInput> input)
        {
            return input
                .Select(Apply)
                .DistinctUntilChanged();
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TInput">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TOutput">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TInput, TSelectorResult1, TSelectorResult2, TSelectorResult3, TOutput> : ISelectorWithoutProps<TInput, TOutput>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TOutput> ProjectorFunction { get; }

        public MemoizedSelector(
            ISelectorWithoutProps<TInput, TSelectorResult1> selector1,
            ISelectorWithoutProps<TInput, TSelectorResult2> selector2,
            ISelectorWithoutProps<TInput, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TOutput> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            ProjectorFunction = projectorFunction;
        }

        public TOutput Apply(TInput input)
        {
            var selector1Result = Selector1.Apply(input);
            var selector2Result = Selector2.Apply(input);
            var selector3Result = Selector3.Apply(input);

            return ProjectorFunction(
                selector1Result,
                selector2Result,
                selector3Result
            );
        }
        public IObservable<TOutput> Apply(IObservable<TInput> input)
        {
            return input
                .Select(Apply)
                .DistinctUntilChanged();
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TInput">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
    /// <typeparam name="TOutput">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TInput, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TOutput> : ISelectorWithoutProps<TInput, TOutput>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Fourth Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult4> Selector4 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TOutput> ProjectorFunction { get; }

        public MemoizedSelector(
            ISelectorWithoutProps<TInput, TSelectorResult1> selector1,
            ISelectorWithoutProps<TInput, TSelectorResult2> selector2,
            ISelectorWithoutProps<TInput, TSelectorResult3> selector3,
            ISelectorWithoutProps<TInput, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TOutput> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            Selector4 = selector4;
            ProjectorFunction = projectorFunction;
        }

        public TOutput Apply(TInput input)
        {
            var selector1Result = Selector1.Apply(input);
            var selector2Result = Selector2.Apply(input);
            var selector3Result = Selector3.Apply(input);
            var selector4Result = Selector4.Apply(input);

            return ProjectorFunction(
                selector1Result,
                selector2Result,
                selector3Result,
                selector4Result
            );
        }
        public IObservable<TOutput> Apply(IObservable<TInput> input)
        {
            return input
                .Select(Apply)
                .DistinctUntilChanged();
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TInput">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
    /// <typeparam name="TSelectorResult5">Result of the fifth selector</typeparam>
    /// <typeparam name="TOutput">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TInput, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TOutput> : ISelectorWithoutProps<TInput, TOutput>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Fourth Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult4> Selector4 { get; }
        /// <summary>
        /// Fifth Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult5> Selector5 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TOutput> ProjectorFunction { get; }

        public MemoizedSelector(
            ISelectorWithoutProps<TInput, TSelectorResult1> selector1,
            ISelectorWithoutProps<TInput, TSelectorResult2> selector2,
            ISelectorWithoutProps<TInput, TSelectorResult3> selector3,
            ISelectorWithoutProps<TInput, TSelectorResult4> selector4,
            ISelectorWithoutProps<TInput, TSelectorResult5> selector5,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TOutput> projectorFunction
        )
        {
            Selector1 = selector1;
            Selector2 = selector2;
            Selector3 = selector3;
            Selector4 = selector4;
            Selector5 = selector5;
            ProjectorFunction = projectorFunction;
        }

        public TOutput Apply(TInput input)
        {
            var selector1Result = Selector1.Apply(input);
            var selector2Result = Selector2.Apply(input);
            var selector3Result = Selector3.Apply(input);
            var selector4Result = Selector4.Apply(input);
            var selector5Result = Selector5.Apply(input);

            return ProjectorFunction(
                selector1Result,
                selector2Result,
                selector3Result,
                selector4Result,
                selector5Result
            );
        }
        public IObservable<TOutput> Apply(IObservable<TInput> input)
        {
            return input
                .Select(Apply)
                .DistinctUntilChanged();
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TInput">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
    /// <typeparam name="TSelectorResult5">Result of the fifth selector</typeparam>
    /// <typeparam name="TSelectorResult6">Result of the sixth selector</typeparam>
    /// <typeparam name="TOutput">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TInput, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TOutput> : ISelectorWithoutProps<TInput, TOutput>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Fourth Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult4> Selector4 { get; }
        /// <summary>
        /// Fifth Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult5> Selector5 { get; }
        /// <summary>
        /// Sixth Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult6> Selector6 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TOutput> ProjectorFunction { get; }

        public MemoizedSelector(
            ISelectorWithoutProps<TInput, TSelectorResult1> selector1,
            ISelectorWithoutProps<TInput, TSelectorResult2> selector2,
            ISelectorWithoutProps<TInput, TSelectorResult3> selector3,
            ISelectorWithoutProps<TInput, TSelectorResult4> selector4,
            ISelectorWithoutProps<TInput, TSelectorResult5> selector5,
            ISelectorWithoutProps<TInput, TSelectorResult6> selector6,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TOutput> projectorFunction
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

        public TOutput Apply(TInput input)
        {
            var selector1Result = Selector1.Apply(input);
            var selector2Result = Selector2.Apply(input);
            var selector3Result = Selector3.Apply(input);
            var selector4Result = Selector4.Apply(input);
            var selector5Result = Selector5.Apply(input);
            var selector6Result = Selector6.Apply(input);

            return ProjectorFunction(
                selector1Result,
                selector2Result,
                selector3Result,
                selector4Result,
                selector5Result,
                selector6Result
            );
        }
        public IObservable<TOutput> Apply(IObservable<TInput> input)
        {
            return input
                .Select(Apply)
                .DistinctUntilChanged();
        }
    }
    /// <summary>
    /// Memoized selector that decompose the selector functions and the projector function
    /// </summary>
    /// <typeparam name="TInput">State consumed by the selectors</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
    /// <typeparam name="TSelectorResult5">Result of the fifth selector</typeparam>
    /// <typeparam name="TSelectorResult6">Result of the sixth selector</typeparam>
    /// <typeparam name="TSelectorResult7">Result of the seventh selector</typeparam>
    /// <typeparam name="TOutput">Result of the memoized selector</typeparam>
    public sealed class MemoizedSelector<TInput, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TOutput> : ISelectorWithoutProps<TInput, TOutput>
    {
        /// <summary>
        /// First selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult1> Selector1 { get; }
        /// <summary>
        /// Second Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult2> Selector2 { get; }
        /// <summary>
        /// Third Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult3> Selector3 { get; }
        /// <summary>
        /// Fourth Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult4> Selector4 { get; }
        /// <summary>
        /// Fifth Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult5> Selector5 { get; }
        /// <summary>
        /// Sixth Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult6> Selector6 { get; }
        /// <summary>
        /// Seventh Selector function
        /// </summary>
        public ISelectorWithoutProps<TInput, TSelectorResult7> Selector7 { get; }
        /// <summary>
        /// Projector function (final function)
        /// </summary>
        public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TOutput> ProjectorFunction { get; }

        public MemoizedSelector(
            ISelectorWithoutProps<TInput, TSelectorResult1> selector1,
            ISelectorWithoutProps<TInput, TSelectorResult2> selector2,
            ISelectorWithoutProps<TInput, TSelectorResult3> selector3,
            ISelectorWithoutProps<TInput, TSelectorResult4> selector4,
            ISelectorWithoutProps<TInput, TSelectorResult5> selector5,
            ISelectorWithoutProps<TInput, TSelectorResult6> selector6,
            ISelectorWithoutProps<TInput, TSelectorResult7> selector7,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TOutput> projectorFunction
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

        public TOutput Apply(TInput input)
        {
            var selector1Result = Selector1.Apply(input);
            var selector2Result = Selector2.Apply(input);
            var selector3Result = Selector3.Apply(input);
            var selector4Result = Selector4.Apply(input);
            var selector5Result = Selector5.Apply(input);
            var selector6Result = Selector6.Apply(input);
            var selector7Result = Selector7.Apply(input);

            return ProjectorFunction(
                selector1Result,
                selector2Result,
                selector3Result,
                selector4Result,
                selector5Result,
                selector6Result,
                selector7Result
            );
        }
        public IObservable<TOutput> Apply(IObservable<TInput> input)
        {
            return input
                .Select(Apply)
                .DistinctUntilChanged();
        }
    }
}
