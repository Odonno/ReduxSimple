namespace ReduxSimple;

internal enum ActionOrigin
{
    Normal = 0x01,

    Redone = 0x02
}

/// <summary>
/// An enumeration to allow the filtering of actions based on their origin
/// </summary>
[Flags]
public enum ActionOriginFilter
{
    /// <summary>
    /// Only actions normally dispatched from <see cref="ReduxStore.Dispatch(object)"/> method
    /// </summary>
    Normal = ActionOrigin.Normal,

    /// <summary>
    /// Only redone actions dispatched from <see cref="ReduxStore.Redo"/> method
    /// </summary>
    Redone = ActionOrigin.Redone,

    /// <summary>
    /// All actions dispatched on a store
    /// </summary>
    All = Normal | Redone
}
