namespace ReduxSimple;

/// <summary>
/// Memoized selector that decompose the selector functions and the projector function (with props).
/// </summary>
/// <typeparam name="TInput">State consumed by the selectors</typeparam>
/// <typeparam name="TProps">Properties used in selectors</typeparam>
/// <typeparam name="TSelectorResult">Result of the selector</typeparam>
/// <typeparam name="TOutput">Result of the memoized selector</typeparam>
public sealed class MemoizedSelectorWithProps<TInput, TProps, TSelectorResult, TOutput> : ISelectorWithProps<TInput, TProps, TOutput>
{
    /// <summary>
    /// Selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult> Selector { get; }
    /// <summary>
    /// Projector function (final function)
    /// </summary>
    public Func<TSelectorResult, TProps, TOutput> ProjectorFunction { get; }

    public MemoizedSelectorWithProps(
        ISelector<TInput, TSelectorResult> selector,
        Func<TSelectorResult, TProps, TOutput> projectorFunction
    )
    {
        Selector = selector;
        ProjectorFunction = projectorFunction;
    }

    public TOutput Apply(TInput input, TProps props)
    {
        var selectorWithoutProps = Selector as ISelectorWithoutProps<TInput, TSelectorResult>;
        var selectorWithProps = Selector as ISelectorWithProps<TInput, TProps, TSelectorResult>;
        var selectorResult = selectorWithoutProps != null
            ? selectorWithoutProps.Apply(input)
            : (selectorWithProps != null
                ? selectorWithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        return ProjectorFunction(selectorResult, props);
    }
    public IObservable<TOutput> Apply(IObservable<TInput> input, TProps props)
    {
        var selectorWithoutProps = Selector as ISelectorWithoutProps<TInput, TSelectorResult>;
        var selectorWithProps = Selector as ISelectorWithProps<TInput, TProps, TSelectorResult>;
        var selectorResult = selectorWithoutProps?.Apply(input) 
            ?? selectorWithProps?.Apply(input, props) 
            ?? throw new InvalidOperationException();

        return selectorResult
            .Select(value => ProjectorFunction(value, props))
            .DistinctUntilChanged();
    }
}
/// <summary>
/// Memoized selector that decompose the selector functions and the projector function (with props)
/// </summary>
/// <typeparam name="TInput">State consumed by the selectors</typeparam>
/// <typeparam name="TProps">Properties used in selectors</typeparam>
/// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
/// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
/// <typeparam name="TOutput">Result of the memoized selector</typeparam>
public sealed class MemoizedSelectorWithProps<TInput, TProps, TSelectorResult1, TSelectorResult2, TOutput> : ISelectorWithProps<TInput, TProps, TOutput>
{
    /// <summary>
    /// First selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult1> Selector1 { get; }
    /// <summary>
    /// Second Selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult2> Selector2 { get; }
    /// <summary>
    /// Projector function (final function)
    /// </summary>
    public Func<TSelectorResult1, TSelectorResult2, TProps, TOutput> ProjectorFunction { get; }

    public MemoizedSelectorWithProps(
        ISelector<TInput, TSelectorResult1> selector1,
        ISelector<TInput, TSelectorResult2> selector2,
        Func<TSelectorResult1, TSelectorResult2, TProps, TOutput> projectorFunction
    )
    {
        Selector1 = selector1;
        Selector2 = selector2;
        ProjectorFunction = projectorFunction;
    }

    public TOutput Apply(TInput input, TProps props)
    {
        var selector1WithoutProps = Selector1 as ISelectorWithoutProps<TInput, TSelectorResult1>;
        var selector1WithProps = Selector1 as ISelectorWithProps<TInput, TProps, TSelectorResult1>;
        var selector1Result = selector1WithoutProps != null
            ? selector1WithoutProps.Apply(input)
            : (selector1WithProps != null
                ? selector1WithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        var selector2WithoutProps = Selector2 as ISelectorWithoutProps<TInput, TSelectorResult2>;
        var selector2WithProps = Selector2 as ISelectorWithProps<TInput, TProps, TSelectorResult2>;
        var selector2Result = selector2WithoutProps != null
            ? selector2WithoutProps.Apply(input)
            : (selector2WithProps != null
                ? selector2WithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        return ProjectorFunction(
            selector1Result,
            selector2Result,
            props
        );
    }
    public IObservable<TOutput> Apply(IObservable<TInput> input, TProps props)
    {
        var selector1WithoutProps = Selector1 as ISelectorWithoutProps<TInput, TSelectorResult1>;
        var selector1WithProps = Selector1 as ISelectorWithProps<TInput, TProps, TSelectorResult1>;
        var selector1Result = selector1WithoutProps?.Apply(input)
            ?? selector1WithProps?.Apply(input, props)
            ?? throw new InvalidOperationException();

        var selector2WithoutProps = Selector2 as ISelectorWithoutProps<TInput, TSelectorResult2>;
        var selector2WithProps = Selector2 as ISelectorWithProps<TInput, TProps, TSelectorResult2>;
        var selector2Result = selector2WithoutProps?.Apply(input)
            ?? selector2WithProps?.Apply(input, props)
            ?? throw new InvalidOperationException();

        return Observable.CombineLatest(
            selector1Result,
            selector2Result,
            Tuple.Create
        )
            .Select(x =>
                ProjectorFunction(
                    x.Item1,
                    x.Item2,
                    props
                )
            )
            .DistinctUntilChanged();
    }
}
/// <summary>
/// Memoized selector that decompose the selector functions and the projector function (with props)
/// </summary>
/// <typeparam name="TInput">State consumed by the selectors</typeparam>
/// <typeparam name="TProps">Properties used in selectors</typeparam>
/// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
/// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
/// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
/// <typeparam name="TOutput">Result of the memoized selector</typeparam>
public sealed class MemoizedSelectorWithProps<TInput, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TOutput> : ISelectorWithProps<TInput, TProps, TOutput>
{
    /// <summary>
    /// First selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult1> Selector1 { get; }
    /// <summary>
    /// Second Selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult2> Selector2 { get; }
    /// <summary>
    /// Third Selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult3> Selector3 { get; }
    /// <summary>
    /// Projector function (final function)
    /// </summary>
    public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TOutput> ProjectorFunction { get; }

    public MemoizedSelectorWithProps(
        ISelector<TInput, TSelectorResult1> selector1,
        ISelector<TInput, TSelectorResult2> selector2,
        ISelector<TInput, TSelectorResult3> selector3,
        Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TOutput> projectorFunction
    )
    {
        Selector1 = selector1;
        Selector2 = selector2;
        Selector3 = selector3;
        ProjectorFunction = projectorFunction;
    }

    public TOutput Apply(TInput input, TProps props)
    {
        var selector1WithoutProps = Selector1 as ISelectorWithoutProps<TInput, TSelectorResult1>;
        var selector1WithProps = Selector1 as ISelectorWithProps<TInput, TProps, TSelectorResult1>;
        var selector1Result = selector1WithoutProps != null
            ? selector1WithoutProps.Apply(input)
            : (selector1WithProps != null
                ? selector1WithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        var selector2WithoutProps = Selector2 as ISelectorWithoutProps<TInput, TSelectorResult2>;
        var selector2WithProps = Selector2 as ISelectorWithProps<TInput, TProps, TSelectorResult2>;
        var selector2Result = selector2WithoutProps != null
            ? selector2WithoutProps.Apply(input)
            : (selector2WithProps != null
                ? selector2WithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        var selector3WithoutProps = Selector3 as ISelectorWithoutProps<TInput, TSelectorResult3>;
        var selector3WithProps = Selector3 as ISelectorWithProps<TInput, TProps, TSelectorResult3>;
        var selector3Result = selector3WithoutProps != null
            ? selector3WithoutProps.Apply(input)
            : (selector3WithProps != null
                ? selector3WithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        return ProjectorFunction(
            selector1Result,
            selector2Result,
            selector3Result,
            props
        );
    }
    public IObservable<TOutput> Apply(IObservable<TInput> input, TProps props)
    {
        var selector1WithoutProps = Selector1 as ISelectorWithoutProps<TInput, TSelectorResult1>;
        var selector1WithProps = Selector1 as ISelectorWithProps<TInput, TProps, TSelectorResult1>;
        var selector1Result = selector1WithoutProps?.Apply(input)
            ?? selector1WithProps?.Apply(input, props)
            ?? throw new InvalidOperationException();

        var selector2WithoutProps = Selector2 as ISelectorWithoutProps<TInput, TSelectorResult2>;
        var selector2WithProps = Selector2 as ISelectorWithProps<TInput, TProps, TSelectorResult2>;
        var selector2Result = selector2WithoutProps?.Apply(input)
            ?? selector2WithProps?.Apply(input, props)
            ?? throw new InvalidOperationException();

        var selector3WithoutProps = Selector3 as ISelectorWithoutProps<TInput, TSelectorResult3>;
        var selector3WithProps = Selector3 as ISelectorWithProps<TInput, TProps, TSelectorResult3>;
        var selector3Result = selector3WithoutProps?.Apply(input)
            ?? selector3WithProps?.Apply(input, props)
            ?? throw new InvalidOperationException();

        return Observable.CombineLatest(
            selector1Result,
            selector2Result,
            selector3Result,
            Tuple.Create
        )
            .Select(x =>
                ProjectorFunction(
                    x.Item1,
                    x.Item2,
                    x.Item3,
                    props
                )
            )
            .DistinctUntilChanged();
    }
}
/// <summary>
/// Memoized selector that decompose the selector functions and the projector function (with props)
/// </summary>
/// <typeparam name="TInput">State consumed by the selectors</typeparam>
/// <typeparam name="TProps">Properties used in selectors</typeparam>
/// <typeparam name="TSelectorResult1">Result of the first selector</typeparam>
/// <typeparam name="TSelectorResult2">Result of the second selector</typeparam>
/// <typeparam name="TSelectorResult3">Result of the third selector</typeparam>
/// <typeparam name="TSelectorResult4">Result of the fourth selector</typeparam>
/// <typeparam name="TOutput">Result of the memoized selector</typeparam>
public sealed class MemoizedSelectorWithProps<TInput, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TOutput> : ISelectorWithProps<TInput, TProps, TOutput>
{
    /// <summary>
    /// First selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult1> Selector1 { get; }
    /// <summary>
    /// Second Selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult2> Selector2 { get; }
    /// <summary>
    /// Third Selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult3> Selector3 { get; }
    /// <summary>
    /// Fourth Selector function
    /// </summary>
    public ISelector<TInput, TSelectorResult4> Selector4 { get; }
    /// <summary>
    /// Projector function (final function)
    /// </summary>
    public Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TOutput> ProjectorFunction { get; }

    public MemoizedSelectorWithProps(
        ISelector<TInput, TSelectorResult1> selector1,
        ISelector<TInput, TSelectorResult2> selector2,
        ISelector<TInput, TSelectorResult3> selector3,
        ISelector<TInput, TSelectorResult4> selector4,
        Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TOutput> projectorFunction
    )
    {
        Selector1 = selector1;
        Selector2 = selector2;
        Selector3 = selector3;
        Selector4 = selector4;
        ProjectorFunction = projectorFunction;
    }

    public TOutput Apply(TInput input, TProps props)
    {
        var selector1WithoutProps = Selector1 as ISelectorWithoutProps<TInput, TSelectorResult1>;
        var selector1WithProps = Selector1 as ISelectorWithProps<TInput, TProps, TSelectorResult1>;
        var selector1Result = selector1WithoutProps != null
            ? selector1WithoutProps.Apply(input)
            : (selector1WithProps != null
                ? selector1WithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        var selector2WithoutProps = Selector2 as ISelectorWithoutProps<TInput, TSelectorResult2>;
        var selector2WithProps = Selector2 as ISelectorWithProps<TInput, TProps, TSelectorResult2>;
        var selector2Result = selector2WithoutProps != null
            ? selector2WithoutProps.Apply(input)
            : (selector2WithProps != null
                ? selector2WithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        var selector3WithoutProps = Selector3 as ISelectorWithoutProps<TInput, TSelectorResult3>;
        var selector3WithProps = Selector3 as ISelectorWithProps<TInput, TProps, TSelectorResult3>;
        var selector3Result = selector3WithoutProps != null
            ? selector3WithoutProps.Apply(input)
            : (selector3WithProps != null
                ? selector3WithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        var selector4WithoutProps = Selector4 as ISelectorWithoutProps<TInput, TSelectorResult4>;
        var selector4WithProps = Selector4 as ISelectorWithProps<TInput, TProps, TSelectorResult4>;
        var selector4Result = selector4WithoutProps != null
            ? selector4WithoutProps.Apply(input)
            : (selector4WithProps != null
                ? selector4WithProps.Apply(input, props)
                : throw new InvalidOperationException()
            );

        return ProjectorFunction(
            selector1Result,
            selector2Result,
            selector3Result,
            selector4Result,
            props
        );
    }
    public IObservable<TOutput> Apply(IObservable<TInput> input, TProps props)
    {
        var selector1WithoutProps = Selector1 as ISelectorWithoutProps<TInput, TSelectorResult1>;
        var selector1WithProps = Selector1 as ISelectorWithProps<TInput, TProps, TSelectorResult1>;
        var selector1Result = selector1WithoutProps?.Apply(input)
            ?? selector1WithProps?.Apply(input, props)
            ?? throw new InvalidOperationException();

        var selector2WithoutProps = Selector2 as ISelectorWithoutProps<TInput, TSelectorResult2>;
        var selector2WithProps = Selector2 as ISelectorWithProps<TInput, TProps, TSelectorResult2>;
        var selector2Result = selector2WithoutProps?.Apply(input)
            ?? selector2WithProps?.Apply(input, props)
            ?? throw new InvalidOperationException();

        var selector3WithoutProps = Selector3 as ISelectorWithoutProps<TInput, TSelectorResult3>;
        var selector3WithProps = Selector3 as ISelectorWithProps<TInput, TProps, TSelectorResult3>;
        var selector3Result = selector3WithoutProps?.Apply(input)
            ?? selector3WithProps?.Apply(input, props)
            ?? throw new InvalidOperationException();

        var selector4WithoutProps = Selector4 as ISelectorWithoutProps<TInput, TSelectorResult4>;
        var selector4WithProps = Selector4 as ISelectorWithProps<TInput, TProps, TSelectorResult4>;
        var selector4Result = selector4WithoutProps?.Apply(input)
            ?? selector4WithProps?.Apply(input, props)
            ?? throw new InvalidOperationException();

        return Observable.CombineLatest(
            selector1Result,
            selector2Result,
            selector3Result,
            selector4Result,
            Tuple.Create
        )
            .Select(x =>
                ProjectorFunction(
                    x.Item1,
                    x.Item2,
                    x.Item3,
                    x.Item4,
                    props
                )
            )
            .DistinctUntilChanged();
    }
}
