using ReduxSimple.Samples.Counter;
using ReduxSimple.Samples.Extensions;
using ReduxSimple.Samples.Pokedex;
using ReduxSimple.Samples.TicTacToe;
using ReduxSimple.Samples.TodoList;
using System;
using System.Reactive.Linq;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using static Microsoft.Toolkit.Uwp.UI.Extensions.ApplicationViewExtensions;

namespace ReduxSimple.Samples
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            GoToCounterButton.ObserveOnClick()
                .Subscribe(_ => Frame.Navigate(typeof(CounterPage)));

            GoToTicTacToeButton.ObserveOnClick()
                .Subscribe(_ => Frame.Navigate(typeof(TicTacToePage)));

            GoToTodoListButton.ObserveOnClick()
                .Subscribe(_ => Frame.Navigate(typeof(TodoListPage)));

            GoToPokedexButton.ObserveOnClick()
                .Subscribe(_ => Frame.Navigate(typeof(PokedexPage)));

            // Extend view into title bar
            SetExtendViewIntoTitleBar(this, true);

            // Set TitleBar properties (colors)
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
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
                SystemNavigationManager.GetForCurrentView().ObserveOnBackRequested()
                    .Where(_ => Frame.CanGoBack)
                    .Subscribe(_ => Frame.GoBack());

                // Show back button when required
                Frame.ObserveOnNavigated()
                    .Subscribe(_ =>
                    {
                        SetBackButtonVisibility(this,
                            Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed);
                    });
            }
        }
    }
}
