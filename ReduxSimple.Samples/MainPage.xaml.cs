using ReduxSimple.Samples.Counter;
using ReduxSimple.Samples.Pokedex;
using ReduxSimple.Samples.TicTacToe;
using ReduxSimple.Samples.TodoList;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ReduxSimple.Samples
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => GoToCounterButton.Click += h,
                h => GoToCounterButton.Click -= h
            )
                .Subscribe(_ =>
                {
                    Frame.Navigate(typeof(CounterPage));
                });

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => GoToTicTacToeButton.Click += h,
                h => GoToTicTacToeButton.Click -= h
            )
                .Subscribe(_ =>
                {
                    Frame.Navigate(typeof(TicTacToePage));
                });

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => GoToTodoListButton.Click += h,
                h => GoToTodoListButton.Click -= h
            )
                .Subscribe(_ =>
                {
                    Frame.Navigate(typeof(TodoListPage));
                });

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => GoToPokedexButton.Click += h,
                h => GoToPokedexButton.Click -= h
            )
                .Subscribe(_ =>
                {
                    Frame.Navigate(typeof(PokedexPage));
                });
        }
    }
}
