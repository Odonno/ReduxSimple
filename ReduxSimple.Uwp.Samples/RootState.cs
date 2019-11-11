using ReduxSimple.Uwp.Samples.Counter;
using ReduxSimple.Uwp.Samples.Pokedex;
using ReduxSimple.Uwp.Samples.TicTacToe;
using ReduxSimple.Uwp.Samples.TodoList;
using ReduxSimple.Uwp.RouterStore;

namespace ReduxSimple.Uwp.Samples
{
    public class RootState : IBaseRouterState
    {
        public RouterState Router { get; set; }

        public CounterState Counter { get; set; }
        public TicTacToeState TicTacToe { get; set; }
        public TodoListState TodoList { get; set; }
        public PokedexState Pokedex { get; set; }

        public static RootState InitialState =>
            new RootState
            {
                Router = RouterState.InitialState,
                Counter = CounterState.InitialState,
                TicTacToe = TicTacToeState.InitialState,
                TodoList = TodoListState.InitialState,
                Pokedex = PokedexState.InitialState
            };
    }
}
