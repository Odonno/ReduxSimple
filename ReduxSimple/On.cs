using System;

namespace ReduxSimple
{
    /// <summary>
    /// A class that contains reducer with filtering options.
    /// </summary>
    /// <typeparam name="TState">State type used in the reducer function.</typeparam>
    public class On<TState> where TState : class
    {
        /// <summary>
        /// Reducer function
        /// </summary>
        public Func<TState, object, TState> Reduce { get; set; }

        /// <summary>
        /// List of action type that target this reducer
        /// </summary>
        public string[] Types { get; set; }
    }
}
