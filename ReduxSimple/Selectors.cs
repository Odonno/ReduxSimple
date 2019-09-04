using System;

namespace ReduxSimple
{
    public static class Selectors
    {
        #region Simple selectors

        /// <summary>
        /// Create a new selector based on the previous ones.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TSelectorResult1">Result of the previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TSelectorResult1, TFinalResult> finalSelector
        )
        {
            return state => finalSelector(selector1(state));
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TFinalResult> finalSelector
        )
        {
            return state => finalSelector(
                selector1(state),
                selector2(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> finalSelector
        )
        {
            return state => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return state => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state),
                selector4(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TState, TSelectorResult5> selector5,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TFinalResult> finalSelector
        )
        {
            return state => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state),
                selector4(state),
                selector5(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TState, TSelectorResult5> selector5,
            Func<TState, TSelectorResult6> selector6,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TFinalResult> finalSelector
        )
        {
            return state => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state),
                selector4(state),
                selector5(state),
                selector6(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TState, TSelectorResult5> selector5,
            Func<TState, TSelectorResult6> selector6,
            Func<TState, TSelectorResult7> selector7,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TFinalResult> finalSelector
        )
        {
            return state => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state),
                selector4(state),
                selector5(state),
                selector6(state),
                selector7(state)
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
        /// <typeparam name="TSelectorResult8">Result of the eight previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="selector2">Second selector.</param>
        /// <param name="selector3">Third selector.</param>
        /// <param name="selector4">Fourth selector.</param>
        /// <param name="selector5">Fifth selector.</param>
        /// <param name="selector6">Sixth selector.</param>
        /// <param name="selector7">Seventh selector.</param>
        /// <param name="selector8">Eight selector.</param>
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TFinalResult> CreateSelector<TState, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TSelectorResult8, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TState, TSelectorResult5> selector5,
            Func<TState, TSelectorResult6> selector6,
            Func<TState, TSelectorResult7> selector7,
            Func<TState, TSelectorResult8> selector8,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TSelectorResult5, TSelectorResult6, TSelectorResult7, TSelectorResult8, TFinalResult> finalSelector
        )
        {
            return state => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state),
                selector4(state),
                selector5(state),
                selector6(state),
                selector7(state),
                selector8(state)
            );
        }

        #endregion

        #region Selectors with props (1 selector)

        /// <summary>
        /// Create a new selector based on the previous ones.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
        /// <typeparam name="TSelectorResult1">Result of the previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TSelectorResult1, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(selector1(state, props));
        }

        /// <summary>
        /// Create a new selector based on the previous ones.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
        /// <typeparam name="TSelectorResult1">Result of the previous selector.</typeparam>
        /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
        /// <param name="selector1">First selector.</param>
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TSelectorResult1, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props)
            );
        }/// <summary>
         /// Create a new selector based on the previous ones.
         /// </summary>
         /// <typeparam name="TState">State to consume.</typeparam>
         /// <typeparam name="TProps">Properties to pass to every selector.</typeparam>
         /// <typeparam name="TSelectorResult1">Result of the first previous selector.</typeparam>
         /// <typeparam name="TSelectorResult2">Result of the second previous selector.</typeparam>
         /// <typeparam name="TFinalResult">Result of the final selector.</typeparam>
         /// <param name="selector1">First selector.</param>
         /// <param name="selector2">Second selector.</param>
         /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
         /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TSelectorResult1, TSelectorResult2, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state, props),
                selector4(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state),
                selector4(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state),
                selector4(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state, props),
                selector4(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state),
                selector4(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state),
                selector4(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state, props),
                selector4(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state),
                selector4(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state, props),
                selector4(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state),
                selector4(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state, props),
                selector4(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state, props),
                selector4(state)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state, props),
                selector4(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state, props),
                selector4(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state),
                selector4(state, props)
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state, props),
                selector4(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state),
                selector4(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state),
                selector4(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state, props),
                selector4(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state),
                selector4(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state),
                selector4(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state, props),
                selector4(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state),
                selector4(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state, props),
                selector4(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state),
                selector4(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state),
                selector3(state, props),
                selector4(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state, props),
                selector4(state),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state),
                selector2(state, props),
                selector3(state, props),
                selector4(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TSelectorResult2> selector2,
            Func<TState, TProps, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state),
                selector3(state, props),
                selector4(state, props),
                props
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
        /// <param name="finalSelector">Selector that combines all values from the previous selectors.</param>
        /// <returns>A new selector using the previous ones.</returns>
        public static Func<TState, TProps, TFinalResult> CreateSelector<TState, TProps, TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TFinalResult>(
            Func<TState, TProps, TSelectorResult1> selector1,
            Func<TState, TProps, TSelectorResult2> selector2,
            Func<TState, TSelectorResult3> selector3,
            Func<TState, TProps, TSelectorResult4> selector4,
            Func<TSelectorResult1, TSelectorResult2, TSelectorResult3, TSelectorResult4, TProps, TFinalResult> finalSelector
        )
        {
            return (state, props) => finalSelector(
                selector1(state, props),
                selector2(state, props),
                selector3(state),
                selector4(state, props),
                props
            );
        }

        #endregion
    }
}
