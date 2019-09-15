using System;

namespace ReduxSimple
{
    public static class Effects
    {
        /// <summary>
        /// Create an effect for a specific state (ie. an <see cref="IObservable{object}"/> that can be use to dispatch new actions asynchronously).
        /// </summary>
        /// <typeparam name="TState">Type of the state.</typeparam>
        /// <param name="run">A function that returns the <see cref="IObservable{object}"/> effect.</param>
        /// <param name="dispatch">Specify if the run function should dispatch the output to the Store.</param>
        /// <returns>The generated effect.</returns>
        public static Effect<TState> CreateEffect<TState>(
            Func<IObservable<object>> run, 
            bool dispatch
        )
            where TState : class, new()
        {
            return new Effect<TState>
            {
                Run = run,
                Config = new EffectConfig
                {
                    Dispatch = dispatch
                }
            };
        }
    }
}
