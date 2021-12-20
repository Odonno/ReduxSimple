namespace ReduxSimple;

/// <summary>
/// A class that represents a previous dispatched action, with details like the related state updated at that time.
/// </summary>
/// <typeparam name="TState">Type of the state.</typeparam>
public class ReduxMemento<TState> where TState : class, new()
{
    /// <summary>
    /// The date when the action was dispatched.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The state before the action was dispatched in the store.
    /// </summary>
    public TState PreviousState { get; }

    /// <summary>
    /// The action dispatched.
    /// </summary>
    public object Action { get; }

    /// <summary>
    /// Creates a new Redux memento.
    /// </summary>
    /// <param name="date">The date when the action was dispatched.</param>
    /// <param name="state">The state updated according to the dispatched action.</param>
    /// <param name="action">The action dispatched.</param>
    public ReduxMemento(DateTime date, TState state, object action)
    {
        Date = date;
        PreviousState = state;
        Action = action;
    }
}
