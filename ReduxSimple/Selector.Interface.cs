namespace ReduxSimple;

/// <summary>
/// The base interface for every type of selector.
/// </summary>
public interface ISelector<in TInput, out TOutput>
{
}

/// <summary>
/// The base interface for every type of selector (that forbid use of props).
/// </summary>
public interface ISelectorWithoutProps<in TInput, out TOutput> : ISelector<TInput, TOutput>
{
    /// <summary>
    /// Apply the selector to the input.
    /// </summary>
    /// <param name="input">Input of the selector.</param>
    /// <returns></returns>
    TOutput Apply(TInput input);
    /// <summary>
    /// Apply the selector on an observable.
    /// </summary>
    /// <param name="input">Observable of values as input.</param>
    /// <returns></returns>
    IObservable<TOutput> Apply(IObservable<TInput> input);
}

/// <summary>
/// The base interface for every type of selector (with props).
/// </summary>
public interface ISelectorWithProps<in TInput, in TProps, out TOutput> : ISelector<TInput, TOutput>
{
    /// <summary>
    /// Apply the selector to the input.
    /// </summary>
    /// <param name="input">Input of the selector.</param>
    /// <param name="props">Properties used in the selector.</param>
    /// <returns></returns>
    TOutput Apply(TInput input, TProps props);
    /// <summary>
    /// Apply the selector on an observable.
    /// </summary>
    /// <param name="input">Observable of values as input.</param>
    /// <param name="props">Properties used in the selector.</param>
    /// <returns></returns>
    IObservable<TOutput> Apply(IObservable<TInput> input, TProps props);
}
