using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace ReduxSimple
{
    /// <summary>
    /// The <see cref="ReduxStore{TState}" /> is a centralized object for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public sealed partial class ReduxStore<TState> where TState : class, new()
    {
        private readonly List<Effect<TState>> _effects = new List<Effect<TState>>();

        /// <summary>
        /// Append effects to the current list of effect.
        /// </summary>
        /// <param name="effect">Effect to use in the Store.</param>
        public void RegisterEffects(params Effect<TState>[] effects)
        {
            foreach (var effect in effects)
            {
                if (effect.Config.Dispatch)
                {
                    effect.Run().Subscribe(Dispatch);
                }
                else
                {
                    effect.Run().Subscribe();
                }

                _effects.Add(effect);
            }
        }
    }
}