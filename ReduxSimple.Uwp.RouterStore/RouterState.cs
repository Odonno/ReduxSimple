namespace ReduxSimple.Uwp.RouterStore
{
    /// <summary>
    /// State used to display information about the Router Store.
    /// </summary>
    public class RouterState
    {
        /// <summary>
        /// Informs if the user can go back.
        /// </summary>
        public bool CanGoBack { get; set; }

        /// <summary>
        /// Informs if the user can go forward.
        /// </summary>
        public bool CanGoForward { get; set; }

        /// <summary>
        /// Initial state of the router state.
        /// </summary>
        public static RouterState InitialState =>
            new RouterState
            {
                CanGoBack = false,
                CanGoForward = false
            };
    }
}
