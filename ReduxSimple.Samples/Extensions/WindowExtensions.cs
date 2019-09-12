using ReduxSimple.Samples.Components;
using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace ReduxSimple.Samples.Extensions
{
    public static class WindowExtensions
    {
        public static async Task OpenNewWindowAsync(Type pageType, object parameter = null)
        {
            var appWindow = await AppWindow.TryCreateAsync();

            var appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(pageType, parameter);

            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);

            await appWindow.TryShowAsync();

            appWindow.Closed += delegate
            {
                appWindowContentFrame.Content = null;
                appWindow = null;
            };
        }

        public static async Task OpenDevToolsAsync<TState>(ReduxStoreWithHistory<TState> store = null)
            where TState : class, new()
        {
            var appWindow = await AppWindow.TryCreateAsync();

            var appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(DevToolsComponent), store);

            var devToolsComponent = appWindowContentFrame.Content as DevToolsComponent;
            devToolsComponent.Initialize(store);

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

            await appWindow.TryShowAsync();

            appWindow.Closed += delegate
            {
                appWindowContentFrame.Content = null;
                appWindow = null;
            };
        }
    }
}
