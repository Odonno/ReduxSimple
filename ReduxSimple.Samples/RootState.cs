using ReduxSimple.Samples.Counter;
using ReduxSimple.Samples.Pokedex;
using ReduxSimple.Samples.Router;
using ReduxSimple.Samples.TicTacToe;
using ReduxSimple.Samples.TodoList;

namespace ReduxSimple.Samples
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
