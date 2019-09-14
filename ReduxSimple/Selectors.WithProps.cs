using System;

namespace ReduxSimple
{
    public static partial class Selectors
    {
        #region Selectors with props (1 selector)

        /// <summary>
        /// Create a new selector based on the previous ones.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
        /// <typeparam name="TSelectorResult1">Result of the previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TSelectorResult1, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TFinalResult>(
                selector1,
                (result1, props) => projectorFunction(result1)
            );
        }

        /// <summary>
        /// Create a new selector based on the previous ones.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
        /// <typeparam name="TSelectorResult1">Result of the previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TSelectorResult1, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TFinalResult>(
                (state, props) => selector1(state),
                projectorFunction
            );
        }

        /// <summary>
        /// Create a new selector based on the previous ones.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
        /// <typeparam name="TSelectorResult1">Result of the previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TSelectorResult1, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TFinalResult>(
                selector1,
                projectorFunction
            );
        }

        #endregion
        #region Selectors with props (2 selectors)

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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
               selector1,
               selector2,
               (result1, result2, props) => projectorFunction(result1, result2)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
                (state, props) => selector1(state),
                selector2,
                (result1, result2, props) => projectorFunction(result1, result2)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
                (result1, result2, props) => projectorFunction(result1, result2)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
                (state, props) => selector1(state),
                (state, props) => selector2(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
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
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="selector2">Second selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
                (state, props) => selector1(state),
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
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="selector2">Second selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
                projectorFunction
            );
        }

        #endregion
        #region Selectors with props (3 selectors)

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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
               selector1,
               selector2,
               selector3,
               (result1, result2, result3, props) => projectorFunction(result1, result2, result3)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
               selector1,
               (state, props) => selector2(state),
               (state, props) => selector3(state),
               (result1, result2, result3, props) => projectorFunction(result1, result2, result3)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
               (state, props) => selector1(state),
               selector2,
               (state, props) => selector3(state),
               (result1, result2, result3, props) => projectorFunction(result1, result2, result3)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                (state, props) => selector1(state),
                (state, props) => selector2(state),
                selector3,
                (result1, result2, result3, props) => projectorFunction(result1, result2, result3)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                selector1,
                selector2,
                (state, props) => selector3(state),
                (result1, result2, result3, props) => projectorFunction(result1, result2, result3)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
                selector3,
                (result1, result2, result3, props) => projectorFunction(result1, result2, result3)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                (state, props) => selector1(state),
                selector2,
                selector3,
                (result1, result2, result3, props) => projectorFunction(result1, result2, result3)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                (state, props) => selector1(state),
                (state, props) => selector2(state),
                (state, props) => selector3(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
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
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="selector2">Second selector.</param>
        /// <param name="selector3">Third selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
                (state, props) => selector3(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                (state, props) => selector1(state),
                selector2,
                (state, props) => selector3(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                (state, props) => selector1(state),
                (state, props) => selector2(state),
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
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="selector2">Second selector.</param>
        /// <param name="selector3">Third selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                selector1,
                selector2,
                (state, props) => selector3(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
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
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="selector2">Second selector.</param>
        /// <param name="selector3">Third selector.</param>
        /// <param name="projectorFunction">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
                (state, props) => selector1(state),
                selector2,
                selector3,
                projectorFunction
            );
        }

        #endregion
        #region Selectors with props (4 selectors)

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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               selector1,
               selector2,
               selector3,
               selector4,
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               selector1,
               (state, props) => selector2(state),
               (state, props) => selector3(state),
               (state, props) => selector4(state),
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               (state, props) => selector1(state),
               selector2,
               (state, props) => selector3(state),
               (state, props) => selector4(state),
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               (state, props) => selector1(state),
               (state, props) => selector2(state),
               selector3,
               (state, props) => selector4(state),
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               (state, props) => selector1(state),
               (state, props) => selector2(state),
               (state, props) => selector3(state),
               selector4,
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               selector1,
               selector2,
               (state, props) => selector3(state),
               (state, props) => selector4(state),
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               selector1,
               (state, props) => selector2(state),
               selector3,
               (state, props) => selector4(state),
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               selector1,
               (state, props) => selector2(state),
               (state, props) => selector3(state),
               selector4,
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               (state, props) => selector1(state),
               selector2,
               selector3,
               (state, props) => selector4(state),
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               (state, props) => selector1(state),
               selector2,
               (state, props) => selector3(state),
               selector4,
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               (state, props) => selector1(state),
               (state, props) => selector2(state),
               selector3,
               selector4,
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               selector1,
               selector2,
               selector3,
               (state, props) => selector4(state),
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
               (state, props) => selector1(state),
               selector2,
               selector3,
               selector4,
               (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
                selector3,
                selector4,
                (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                selector2,
                (state, props) => selector3(state),
                selector4,
                (result1, result2, result3, result4, props) => projectorFunction(result1, result2, result3, result4)
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                (state, props) => selector1(state),
                (state, props) => selector2(state),
                (state, props) => selector3(state),
                (state, props) => selector4(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
                (state, props) => selector3(state),
                (state, props) => selector4(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                (state, props) => selector1(state),
                selector2,
                (state, props) => selector3(state),
                (state, props) => selector4(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                (state, props) => selector1(state),
                (state, props) => selector2(state),
                selector3,
                (state, props) => selector4(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                (state, props) => selector1(state),
                (state, props) => selector2(state),
                (state, props) => selector3(state),
                selector4,
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                selector2,
                (state, props) => selector3(state),
                (state, props) => selector4(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
                selector3,
                (state, props) => selector4(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
                (state, props) => selector3(state),
                selector4,
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                (state, props) => selector1(state),
                selector2,
                selector3,
                (state, props) => selector4(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                (state, props) => selector1(state),
                selector2,
                (state, props) => selector3(state),
                selector4,
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                (state, props) => selector1(state),
                (state, props) => selector2(state),
                selector3,
                selector4,
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                selector2,
                selector3,
                (state, props) => selector4(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                (state, props) => selector1(state),
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                (state, props) => selector2(state),
                selector3,
                selector4,
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
        public static MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> projectorFunction
        )
        {
            return new MemoizedSelectorWithProps<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
                selector1,
                selector2,
                (state, props) => selector3(state),
                selector4,
                projectorFunction
            );
        }

        #endregion
    }
}
