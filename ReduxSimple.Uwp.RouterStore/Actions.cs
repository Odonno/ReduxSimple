using Windows.UI.Xaml.Navigation;

namespace ReduxSimple.Uwp.RouterStore
{
    public class RouterNavigating
    {
        public NavigatingCancelEventArgs Event { get; set; } // TODO : Create wrapped class for this event
    }

    public class RouterNavigated
    {
        public NavigationEventArgs Event { get; set; } // TODO : Create wrapped class for this event
    }

    public class RouterError
    {
        public NavigationFailedEventArgs Event { get; set; } // TODO : Create wrapped class for this event
    }

    public class RouterCancel
    {
        public NavigationEventArgs Event { get; set; } // TODO : Create wrapped class for this event
    }
}
