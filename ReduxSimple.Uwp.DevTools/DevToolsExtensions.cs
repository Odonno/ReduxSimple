using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace ReduxSimple.Uwp.DevTools
{
    public static class DevToolsExtensions
    {
        /// <summary>
        /// Open Store DevTools in a separate window.
        /// </summary>
        /// <typeparam name="TState">Type of the state.</typeparam>
        /// <param name="store">Store to display information about.</param>
        /// <returns>True if the Store DevTools has been shown.</returns>
        public static async Task<bool> OpenDevToolsAsync<TState>(this ReduxStore<TState> store)
            where TState : class, new()
        {
            if (store == null || !store.TimeTravelEnabled)
            {
                return false;
            }

            var appWindow = await AppWindow.TryCreateAsync();

            var appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(DevToolsComponent));

            var devToolsComponent = appWindowContentFrame.Content as DevToolsComponent;
            devToolsComponent?.Initialize(store);

            // TODO : Set as options
            // Extend view into title bar
            appWindow.TitleBar.ExtendsContentIntoTitleBar = true;

            // TODO : Set as options
            // Set TitleBar properties (colors)
            appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            appWindow.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(255, 72, 42, 203);
            appWindow.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(200, 72, 42, 203);
            appWindow.TitleBar.ButtonForegroundColor = Colors.Black;

            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);

            bool result = await appWindow.TryShowAsync();

            appWindow.Closed += delegate
            {
                appWindowContentFrame.Content = null;
            };

            return result;
        }
    }
}
