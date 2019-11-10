namespace ReduxSimple.Uwp.RouterStore
{
    public class RouterNavigating
    {
        public RouterNavigatingEvent Event { get; set; }
    }

    public class RouterNavigated
    {
        public RouterNavigatedEvent Event { get; set; }
    }

    public class RouterError
    {
        public RouterErrorEvent Event { get; set; }
    }

    public class RouterCancel
    {
        public RouterCancelEvent Event { get; set; }
    }
}
