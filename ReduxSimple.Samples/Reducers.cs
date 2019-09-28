using System.Collections.Generic;
using System.Linq;
using static ReduxSimple.Reducers;
using static ReduxSimple.Samples.Counter.Selectors;
using static ReduxSimple.Samples.TicTacToe.Selectors;
using static ReduxSimple.Samples.TodoList.Selectors;
using static ReduxSimple.Samples.Pokedex.Selectors;

namespace ReduxSimple.Samples
{
    public static class Reducers
    {
        public static IEnumerable<On<RootState>> CreateReducers()
        {
            var counterReducers = Counter.Reducers.CreateReducers();
            var ticTacToeReducers = TicTacToe.Reducers.CreateReducers();
            var todoListReducers = TodoList.Reducers.CreateReducers();
            var pokedexReducers = Pokedex.Reducers.CreateReducers();

            return CreateSubReducers(counterReducers.ToArray(), SelectCounterState)
                .Concat(CreateSubReducers(ticTacToeReducers.ToArray(), SelectTicTacToeState))
                .Concat(CreateSubReducers(todoListReducers.ToArray(), SelectTodoListState))
                .Concat(CreateSubReducers(pokedexReducers.ToArray(), SelectPokedexState));
        }
    }
}
