using System;
using Windows.UI.Xaml.Navigation;

namespace ReduxSimple.Uwp.RouterStore
{
    /// <summary>
    /// Simplified event to store UWP router navigation event.
    /// </summary>
    public class RouterNavigatingEvent
    {
        public bool Cancel { get; internal set; }
        public NavigationMode NavigationMode { get; internal set; }
        public Type? SourcePageType { get; internal set; }
        public object? Parameter { get; internal set; }
    }

    /// <summary>
    /// Simplified event to store UWP router navigated event.
    /// </summary>
    public class RouterNavigatedEvent
    {
        public Uri? Uri { get; internal set; }
        public Type? ContentType { get; internal set; }
        public NavigationMode NavigationMode { get; internal set; }
        public object? Parameter { get; internal set; }
        public Type? SourcePageType { get; internal set; }
    }

    /// <summary>
    /// Simplified event to store UWP router error event.
    /// </summary>
    public class RouterErrorEvent
    {
        public bool Handled { get; internal set; }
        public Exception? Exception { get; internal set; }
        public Type? SourcePageType { get; internal set; }
    }

    /// <summary>
    /// Simplified event to store UWP router cancel event.
    /// </summary>
    public class RouterCancelEvent
    {
        public Uri? Uri { get; internal set; }
        public Type? ContentType { get; internal set; }
        public NavigationMode NavigationMode { get; internal set; }
        public object? Parameter { get; internal set; }
        public Type? SourcePageType { get; internal set; }
    }
}
