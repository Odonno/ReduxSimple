using ReduxSimple.Samples.Counter;
using ReduxSimple.Samples.Extensions;
using ReduxSimple.Samples.Pokedex;
using ReduxSimple.Samples.TicTacToe;
using ReduxSimple.Samples.TodoList;
using System;
using System.Reactive.Linq;
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

            GoToCounterButton.ObserveOnClick()
                .Subscribe(_ => Frame.Navigate(typeof(CounterPage)));

            GoToTicTacToeButton.ObserveOnClick()
                .Subscribe(_ => Frame.Navigate(typeof(TicTacToePage)));

            GoToTodoListButton.ObserveOnClick()
                .Subscribe(_ => Frame.Navigate(typeof(TodoListPage)));

            GoToPokedexButton.ObserveOnClick()
                .Subscribe(_ => Frame.Navigate(typeof(PokedexPage)));
        }
    }
}
