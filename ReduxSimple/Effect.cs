using System;

namespace ReduxSimple
{
    /// <summary>
    /// Details of an Effect in the ReduxSimple pattern.
    /// </summary>
    public class Effect<TState> where TState : class, new()
    {
        /// <summary>
        /// Run function
        /// </summary>
        public Func<ReduxStore<TState>, IObservable<object>>? Run { get; set; }

        /// <summary>
        /// Effect configuration
        /// </summary>
        public EffectConfig? Config { get; set; }
    }

    /// <summary>
    /// Configuration of an effect
    /// </summary>
    public class EffectConfig
    {
        /// <summary>
        /// Specify if the run function should dispatch the output to the Store.
        /// </summary>
        public bool Dispatch { get; set; }
    }
}
