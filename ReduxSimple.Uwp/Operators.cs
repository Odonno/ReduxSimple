using System;
using System.Reactive.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace ReduxSimple.Uwp
{
    public static class Operators
    {
        /// <summary>
        /// Automatically unsubscribe the current Observable when the page is unloaded.
        /// </summary>
        /// <typeparam name="T">Type of the data inside the current Observable.</typeparam>
        /// <param name="observable">The current Observable.</param>
        /// <param name="page">The page linked to the current Observable.</param>
        /// <returns>The current Observable.</returns>
        public static IObservable<T> UntilDestroyed<T>(this IObservable<T> observable, Page page)
        {
            var unloaded = page.Events().Unloaded;
            return observable.TakeUntil(unloaded);
        }
    }
}
