namespace ReduxSimple.Uwp.RouterStore
{
    /// <summary>
    /// Action dispatched when the router has started a navigation.
    /// </summary>
    public class RouterNavigatingAction
    {
        public RouterNavigatingEvent Event { get; set; }
    }

    /// <summary>
    /// Action dispatched when the router has completed a navigation.
    /// </summary>
    public class RouterNavigatedAction
    {
        public RouterNavigatedEvent Event { get; set; }
    }

    /// <summary>
    /// Action dispatched when an error occured during navigation.
    /// </summary>
    public class RouterErrorAction
    {
        public RouterErrorEvent Event { get; set; }
    }

    /// <summary>
    /// Action dispatched when the navigation was canceled.
    /// </summary>
    public class RouterCancelAction
    {
        public RouterCancelEvent Event { get; set; }
    }
}
