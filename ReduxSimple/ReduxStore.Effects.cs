using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

namespace ReduxSimple
{
    /// <summary>
    /// The <see cref="ReduxStore{TState}" /> is a centralized object for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public sealed partial class ReduxStore<TState> where TState : class, new()
    {
        /// <summary>
        /// Append effects to the current list of effect.
        /// </summary>
        /// <param name="effect">Effect to use in the Store.</param>
        public void RegisterEffects(params Effect<TState>[] effects)
        {
            var effectSources = effects
                .Select(effect =>
                {
                    if (effect.Run == null || effect.Config == null)
                    {
                        Debug.WriteLine($"An effect is not well configured...");
                        return null;
                    }

                    if (effect.Config.Dispatch)
                    {
                        return effect.Run(this)
                            .Retry()
                            .SelectMany(action => Observable.Return(action));
                    }

                    return effect.Run(this)
                        .Retry()
                        .SelectMany(_ => Observable.Empty<object>());
                });

            Observable.Merge(effectSources).Subscribe(Dispatch);
        }
    }
}