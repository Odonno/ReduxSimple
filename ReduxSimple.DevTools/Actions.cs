namespace ReduxSimple.DevTools;

/// <summary>
/// Action dispatched when the state history is updated (list of action/state updates).
/// </summary>
public class HistoryUpdated
{
    public ImmutableList<ReduxActionInfo> CurrentActions { get; set; } = ImmutableList<ReduxActionInfo>.Empty;
    public ImmutableList<ReduxActionInfo> FutureActions { get; set; } = ImmutableList<ReduxActionInfo>.Empty;
}
    
/// <summary>
/// Action dispatched when the user moves the store to a given position (with undo/redo mechanism).
/// </summary>
public class MoveToPositionAction
{
    public int? Position { get; set; }
}
    
/// <summary>
/// Action dispatched when the user selects an action in the list of current (dispatched) actions.
/// </summary>
public class SelectPositionAction
{
    public int Position { get; set; }
}

/// <summary>
/// Toggle play/pause store to forward redone actions.
/// </summary>
public class TogglePlayPauseAction { }
