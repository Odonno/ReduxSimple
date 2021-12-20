namespace ReduxSimple;

public static partial class Selectors
{
    /// <summary>
    /// Create a new selector based on the previous ones.
    /// </summary>
    /// <typeparam name="TState">State to consume.</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the previous selector.</typeparam>
    /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
    /// <returns>A new selector using the previous ones.</returns>
    public static ISelectorWithoutProps<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TFinalResult>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        Func<TSelectorResult1, TFinalResult> projectorFunction
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TFinalResult>(
            selector1,
            projectorFunction
        );
    }
    /// <summary>
    /// Create a new selector based on the previous ones.
    /// </summary>
    /// <typeparam name="TState">State to consume.</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second previous selector.</typeparam>
    /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="selector2">Second selector.</param>
    /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
    /// <returns>A new selector using the previous ones.</returns>
    public static ISelectorWithoutProps<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TFinalResult>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        ISelectorWithoutProps<TState, TSelectorResult2> selector2,
        Func<TSelectorResult1, TSelectorResult2, TFinalResult> projectorFunction
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TFinalResult>(
            selector1,
            selector2,
            projectorFunction
        );
    }
    /// <summary>
    /// Create a new selector based on the previous ones.
    /// </summary>
    /// <typeparam name="TState">State to consume.</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second previous selector.</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third previous selector.</typeparam>
    /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="selector2">Second selector.</param>
    /// <param name="selector3">Third selector.</param>
    /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
    /// <returns>A new selector using the previous ones.</returns>
    public static ISelectorWithoutProps<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        ISelectorWithoutProps<TState, TSelectorResult2> selector2,
        ISelectorWithoutProps<TState, TSelectorResult3> selector3,
        Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> projectorFunction
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                selector1,
                selector2,
                selector3,
                projectorFunction
        );
    }
    /// <summary>
    /// Create a new selector based on the previous ones.
    /// </summary>
    /// <typeparam name="TState">State to consume.</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second previous selector.</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third previous selector.</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth previous selector.</typeparam>
    /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="selector2">Second selector.</param>
    /// <param name="selector3">Third selector.</param>
    /// <param name="selector4">Fourth selector.</param>
    /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
    /// <returns>A new selector using the previous ones.</returns>
    public static ISelectorWithoutProps<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        ISelectorWithoutProps<TState, TSelectorResult2> selector2,
        ISelectorWithoutProps<TState, TSelectorResult3> selector3,
        ISelectorWithoutProps<TState, TSelectorResult4> selector4,
        Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                selector2,
                selector3,
                selector4,
                projectorFunction
        );
    }
    /// <summary>
    /// Create a new selector based on the previous ones.
    /// </summary>
    /// <typeparam name="TState">State to consume.</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second previous selector.</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third previous selector.</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth previous selector.</typeparam>
    /// <typeparam name="TSelectorResult5">Result of the fifth previous selector.</typeparam>
    /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="selector2">Second selector.</param>
    /// <param name="selector3">Third selector.</param>
    /// <param name="selector4">Fourth selector.</param>
    /// <param name="selector5">Fifth selector.</param>
    /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
    /// <returns>A new selector using the previous ones.</returns>
    public static ISelectorWithoutProps<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TFinalResult>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        ISelectorWithoutProps<TState, TSelectorResult2> selector2,
        ISelectorWithoutProps<TState, TSelectorResult3> selector3,
        ISelectorWithoutProps<TState, TSelectorResult4> selector4,
        ISelectorWithoutProps<TState, TSelectorResult5> selector5,
        Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TFinalResult> projectorFunction
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TFinalResult>(
            selector1,
            selector2,
            selector3,
            selector4,
            selector5,
            projectorFunction
        );
    }
    /// <summary>
    /// Create a new selector based on the previous ones.
    /// </summary>
    /// <typeparam name="TState">State to consume.</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second previous selector.</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third previous selector.</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth previous selector.</typeparam>
    /// <typeparam name="TSelectorResult5">Result of the fifth previous selector.</typeparam>
    /// <typeparam name="TSelectorResult6">Result of the sixth previous selector.</typeparam>
    /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="selector2">Second selector.</param>
    /// <param name="selector3">Third selector.</param>
    /// <param name="selector4">Fourth selector.</param>
    /// <param name="selector5">Fifth selector.</param>
    /// <param name="selector6">Sixth selector.</param>
    /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
    /// <returns>A new selector using the previous ones.</returns>
    public static ISelectorWithoutProps<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TFinalResult>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        ISelectorWithoutProps<TState, TSelectorResult2> selector2,
        ISelectorWithoutProps<TState, TSelectorResult3> selector3,
        ISelectorWithoutProps<TState, TSelectorResult4> selector4,
        ISelectorWithoutProps<TState, TSelectorResult5> selector5,
        ISelectorWithoutProps<TState, TSelectorResult6> selector6,
        Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TFinalResult> projectorFunction
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TFinalResult>(
            selector1,
            selector2,
            selector3,
            selector4,
            selector5,
            selector6,
            projectorFunction
        );
    }
    /// <summary>
    /// Create a new selector based on the previous ones.
    /// </summary>
    /// <typeparam name="TState">State to consume.</typeparam>
    /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
    /// <typeparam name="TSelectorResult2">Result of the second previous selector.</typeparam>
    /// <typeparam name="TSelectorResult3">Result of the third previous selector.</typeparam>
    /// <typeparam name="TSelectorResult4">Result of the fourth previous selector.</typeparam>
    /// <typeparam name="TSelectorResult5">Result of the fifth previous selector.</typeparam>
    /// <typeparam name="TSelectorResult6">Result of the sixth previous selector.</typeparam>
    /// <typeparam name="TSelectorResult7">Result of the seventh previous selector.</typeparam>
    /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
    /// <param name="selector1">First selector.</param>
    /// <param name="selector2">Second selector.</param>
    /// <param name="selector3">Third selector.</param>
    /// <param name="selector4">Fourth selector.</param>
    /// <param name="selector5">Fifth selector.</param>
    /// <param name="selector6">Sixth selector.</param>
    /// <param name="selector7">Seventh selector.</param>
    /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
    /// <returns>A new selector using the previous ones.</returns>
    public static ISelectorWithoutProps<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TFinalResult>(
        ISelectorWithoutProps<TState, TSelectorResult1> selector1,
        ISelectorWithoutProps<TState, TSelectorResult2> selector2,
        ISelectorWithoutProps<TState, TSelectorResult3> selector3,
        ISelectorWithoutProps<TState, TSelectorResult4> selector4,
        ISelectorWithoutProps<TState, TSelectorResult5> selector5,
        ISelectorWithoutProps<TState, TSelectorResult6> selector6,
        ISelectorWithoutProps<TState, TSelectorResult7> selector7,
        Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TFinalResult> projectorFunction
    )
    {
        return new MemoizedSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TFinalResult>(
            selector1,
            selector2,
            selector3,
            selector4,
            selector5,
            selector6,
            selector7,
            projectorFunction
        );
    }
}