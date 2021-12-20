namespace ReduxSimple;

/// <summary>
/// A class that represents the current state history.
/// </summary>
/// <typeparam name="TState">Type of the state.</typeparam>
public class ReduxHistory<TState> where TState : class, new()
{
    /// <summary>
    /// List of previous states (action + state = new state).
    /// </summary>
    public List<ReduxMemento<TState>> PreviousStates { get; }

    /// <summary>
    /// List of future actions (that can be redone).
    /// </summary>
    public List<object> FutureActions { get; }

    /// <summary>
    /// Creates a new state history.
    /// </summary>
    /// <param name="previousStates">List of previous states (action + state = new state).</param>
    /// <param name="futureActions">List of future actions (that can be redone).</param>
    public ReduxHistory(List<ReduxMemento<TState>> previousStates, List<object> futureActions)
    {
        PreviousStates = previousStates;
        FutureActions = futureActions;
    }
}