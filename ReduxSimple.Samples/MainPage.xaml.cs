using ReduxSimple.Samples.Counter;
using ReduxSimple.Samples.Pokedex;
using ReduxSimple.Samples.TicTacToe;
using ReduxSimple.Samples.TodoList;
using System;
using System.Reactive.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;
using static Microsoft.Toolkit.Uwp.UI.Extensions.ApplicationViewExtensions;
using static Windows.UI.Core.AppViewBackButtonVisibility;

namespace ReduxSimple.Samples
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            GoToCounterButton.Events().Click
                .Subscribe(_ => Frame.Navigate(typeof(CounterPage)));

            GoToTicTacToeButton.Events().Click
                .Subscribe(_ => Frame.Navigate(typeof(TicTacToePage)));

            GoToTodoListButton.Events().Click
                .Subscribe(_ => Frame.Navigate(typeof(TodoListPage)));

            GoToPokedexButton.Events().Click
                .Subscribe(_ => Frame.Navigate(typeof(PokedexPage)));

            // Extend view into title bar
            SetExtendViewIntoTitleBar(this, true);

            // Set TitleBar properties (colors)
            var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.ButtonHoverBackgroundColor = Color.FromArgb(255, 72, 42, 203);
            titleBar.ButtonPressedBackgroundColor = Color.FromArgb(200, 72, 42, 203);
            titleBar.ButtonForegroundColor = Colors.Black;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Subscribe only once the MainPage is shown at the start of the app
            if (e.NavigationMode == NavigationMode.New)
            {
                // Go back when back button clicked
                var systemNavigationManager = Windows.UI.Core.SystemNavigationManager.GetForCurrentView();

                Observable.FromEventPattern<EventHandler<Windows.UI.Core.BackRequestedEventArgs>, Windows.UI.Core.BackRequestedEventArgs>(
                    h => systemNavigationManager.BackRequested += h,
                    h => systemNavigationManager.BackRequested -= h
                )
                    .Where(_ => Frame.CanGoBack)
                    .Subscribe(_ => Frame.GoBack());

                // Show back button when required
                Frame.Events().Navigated
                   .Subscribe(_ =>
                   {
                       SetBackButtonVisibility(this, Frame.CanGoBack ? Visible : Collapsed);
                   });
            }
        }
    }
}
