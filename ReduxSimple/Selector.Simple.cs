using System;
using System.Reactive.Linq;

namespace ReduxSimple
{
    public sealed class SimpleSelector<TInput, TOutput> : ISelectorWithoutProps<TInput, TOutput>
    {
        /// <summary>
        /// Selector function
        /// </summary>
        public Func<TInput, TOutput> Selector { get; }

        public SimpleSelector(
            Func<TInput, TOutput> selector
        )
        {
            Selector = selector;
        }

        public IObservable<TOutput> Apply(IObservable<TInput> input)
        {
            return input
                .Select(Selector)
                .DistinctUntilChanged();
        }
    }
}
