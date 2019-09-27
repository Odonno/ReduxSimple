using System;

namespace ReduxSimple
{
    public static partial class Selectors
    {
        /// <summary>
        /// Create a new selector based on the previous ones.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
        /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static ISelectorWithProps<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TFinalResult>(
            ISelector<TState, TSelectorResult1> selector1,
            Func<TSelectorResult1, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TFinalResult>(
                selector1,
                projectorFunction
            );
        }
        /// <summary>
        /// Create a new selector based on the previous ones.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
        /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
        /// <typeparam name="TSelectorResult2">Result of the second previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="selector2">Second selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static ISelectorWithProps<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            ISelector<TState, TSelectorResult1> selector1,
            ISelector<TState, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
                selector1,
                selector2,
                projectorFunction
            );
        }
        /// <summary>
        /// Create a new selector based on the previous ones.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
        /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
        /// <typeparam name="TSelectorResult2">Result of the second previous selector.</typeparam>
        /// <typeparam name="TSelectorResult3">Result of the third previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="selector2">Second selector.</param>
        /// <param name="selector3">Third selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static ISelectorWithProps<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            ISelector<TState, TSelectorResult1> selector1,
            ISelector<TState, TSelectorResult2> selector2,
            ISelector<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
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
        /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
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
        public static ISelectorWithProps<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            ISelector<TState, TSelectorResult1> selector1,
            ISelector<TState, TSelectorResult2> selector2,
            ISelector<TState, TSelectorResult3> selector3,
            ISelector<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1, 
                selector2,
                selector3,
                selector4,
                projectorFunction
            );
        }
    }
}
