namespace ReduxSimple;

public static partial class Selectors
{
    /// <summary>
    /// Combine multiple selectors.
    /// </summary>
    /// <typeparam name="TState">Type of the root state.</typeparam>
    /// <typeparam name="TSelectorResult1">Type of the result of the first selector.</typeparam>
    /// <typeparam name="TSelectorResult2">Type of the result of the second selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="selector2">Second selector.</param>
    /// <returns></returns>
    public static ISelectorWithoutProps<TState, Tuple<TSelectorResult1, TSelectorResult2>> CombineSelectors<TState, TSelectorResult1, TSelectorResult2>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        ISelectorWithoutProps<TState, TSelectorResult2> selector2
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, Tuple<TSelectorResult1, TSelectorResult2>>(
            selector1,
            selector2,
            (result1, result2) => Tuple.Create(result1, result2)
        );
    }
    /// <summary>
    /// Combine multiple selectors.
    /// </summary>
    /// <typeparam name="TState">Type of the root state.</typeparam>
    /// <typeparam name="TSelectorResult1">Type of the result of the first selector.</typeparam>
    /// <typeparam name="TSelectorResult2">Type of the result of the second selector.</typeparam>
    /// <typeparam name="TSelectorResult3">Type of the result of the third selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="selector2">Second selector.</param>
    /// <param name="selector3">Third selector.</param>
    /// <returns></returns>
    public static ISelectorWithoutProps<TState, Tuple<TSelectorResult1, TSelectorResult2, TSelectorResult3>> CombineSelectors<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        ISelectorWithoutProps<TState, TSelectorResult2> selector2,
        ISelectorWithoutProps<TState, TSelectorResult3> selector3
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, Tuple<TSelectorResult1, TSelectorResult2, TSelectorResult3>>(
            selector1,
            selector2,
            selector3,
            (result1, result2, result3) => Tuple.Create(result1, result2, result3)
        );
    }
    /// <summary>
    /// Combine multiple selectors.
    /// </summary>
    /// <typeparam name="TState">Type of the root state.</typeparam>
    /// <typeparam name="TSelectorResult1">Type of the result of the first selector.</typeparam>
    /// <typeparam name="TSelectorResult2">Type of the result of the second selector.</typeparam>
    /// <typeparam name="TSelectorResult3">Type of the result of the third selector.</typeparam>
    /// <typeparam name="TSelectorResult4">Type of the result of the fourth selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="selector2">Second selector.</param>
    /// <param name="selector3">Third selector.</param>
    /// <param name="selector4">Fourth selector.</param>
    /// <returns></returns>
    public static ISelectorWithoutProps<TState, Tuple<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4>> CombineSelectors<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        ISelectorWithoutProps<TState, TSelectorResult2> selector2,
        ISelectorWithoutProps<TState, TSelectorResult3> selector3,
        ISelectorWithoutProps<TState, TSelectorResult4> selector4
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, Tuple<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4>>(
            selector1,
            selector2,
            selector3,
            selector4,
            (result1, result2, result3, result4) => Tuple.Create(result1, result2, result3, result4)
        );
    }
}
