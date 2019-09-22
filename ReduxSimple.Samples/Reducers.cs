using System.Collections.Generic;
using System.Linq;
using static ReduxSimple.Reducers;

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

            return CreateSubReducers(counterReducers.ToArray(), (RootState state) => state.Counter)
                .Concat(CreateSubReducers(ticTacToeReducers.ToArray(), (RootState state) => state.TicTacToe))
                .Concat(CreateSubReducers(todoListReducers.ToArray(), (RootState state) => state.TodoList))
                .Concat(CreateSubReducers(pokedexReducers.ToArray(), (RootState state) => state.Pokedex));
        }
    }
}
