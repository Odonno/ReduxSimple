using ReduxSimple.Uwp.Samples.Counter;
using ReduxSimple.Uwp.Samples.Pokedex;
using ReduxSimple.Uwp.Samples.TicTacToe;
using ReduxSimple.Uwp.Samples.TodoList;
using ReduxSimple.Uwp.RouterStore;

namespace ReduxSimple.Uwp.Samples
{
    public class RootState : IBaseRouterState
    {
        public RouterState Router { get; set; } = new RouterState();

        public CounterState Counter { get; set; } = new CounterState();
        public TicTacToeState TicTacToe { get; set; } = new TicTacToeState();
        public TodoListState TodoList { get; set; } = new TodoListState();
        public PokedexState Pokedex { get; set; } = new PokedexState();
    }
}
