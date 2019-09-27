using System;

namespace ReduxSimple
{
    public static partial class Selectors
    {
        /// <summary>
        /// Create a new simple selector.
        /// </summary>
        /// <typeparam name="TState">State to consume.</typeparam>
        /// <typeparam name="TResult">Result of the selector.</typeparam>
        /// <param name="selector">Selector function.</param>
        /// <returns>A new selector.</returns>
        public static ISelectorWithoutProps<TState, TResult> CreateSelector<TState, TResult>(
            Func<TState, TResult> selector
        )
        {
            return new SimpleSelector<TState, TResult>(
                selector
            );
        }
    }
}
