namespace ReduxSimple.Uwp.RouterStore
{
    public class RouterState
    {
        public bool CanGoBack { get; set; }
        public bool CanGoForward { get; set; }

        public static RouterState InitialState =>
            new RouterState
            {
                CanGoBack = false,
                CanGoForward = false
            };
    }
}
