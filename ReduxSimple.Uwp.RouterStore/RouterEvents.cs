using System;
using Windows.UI.Xaml.Navigation;

namespace ReduxSimple.Uwp.RouterStore
{
    public class RouterNavigatingEvent
    {
        public bool Cancel { get; internal set; }
        public NavigationMode NavigationMode { get; internal set; }
        public Type SourcePageType { get; internal set; }
        public object Parameter { get; internal set; }
    }

    public class RouterNavigatedEvent
    {
        public Uri Uri { get; internal set; }
        public Type ContentType { get; internal set; }
        public NavigationMode NavigationMode { get; internal set; }
        public object Parameter { get; internal set; }
        public Type SourcePageType { get; internal set; }
    }

    public class RouterErrorEvent
    {
        public bool Handled { get; internal set; }
        public Exception Exception { get; internal set; }
        public Type SourcePageType { get; internal set; }
    }

    public class RouterCancelEvent
    {
        public Uri Uri { get; internal set; }
        public Type ContentType { get; internal set; }
        public NavigationMode NavigationMode { get; internal set; }
        public object Parameter { get; internal set; }
        public Type SourcePageType { get; internal set; }
    }
}
