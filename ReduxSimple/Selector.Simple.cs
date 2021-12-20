namespace ReduxSimple;

/// <summary>
/// Simple selector based on a simple <see cref="Func{TInput, TOuput}"/>.
/// </summary>
/// <typeparam name="TInput">Type of the input of the selector.</typeparam>
/// <typeparam name="TOutput">Type of the output of the selector.</typeparam>
public sealed class SimpleSelector<TInput, TOutput> : ISelectorWithoutProps<TInput, TOutput>
{
    /// <summary>
    /// Selector function.
    /// </summary>
    public Func<TInput, TOutput> Selector { get; }

    /// <summary>
    /// Creates a new Selector
    /// </summary>
    /// <param name="selector">The selector function.</param>
    public SimpleSelector(
        Func<TInput, TOutput> selector
    )
    {
        Selector = selector;
    }

    /// <summary>
    /// Apply the selector function with the given <paramref name="input"/>.
    /// </summary>
    /// <param name="input">Input of the function.</param>
    /// <returns>The result of the selector function.</returns>
    public TOutput Apply(TInput input)
    {
        return Selector(input);
    }
    /// <summary>
    /// Apply an <see cref="IObservable{TInput}"/> to the selector function.
    /// </summary>
    /// <param name="input">Input of the function.</param>
    /// <returns>The result of the selector function as an <see cref="IObservable{TOutput}"/>.</returns>
    public IObservable<TOutput> Apply(IObservable<TInput> input)
    {
        return input
            .Select(Selector)
            .DistinctUntilChanged();
    }
}
