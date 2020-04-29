using System.Collections.Generic;
using static ReduxSimple.Reducers;

namespace ReduxSimple.Uwp.Samples
{
    public static class Reducers
    {
        public static IEnumerable<On<RootState>> CreateReducers()
        {
            return CombineReducers(
                Counter.Reducers.GetReducers(),
                TicTacToe.Reducers.GetReducers(),
                TodoList.Reducers.GetReducers(),
                Pokedex.Reducers.GetReducers()
            );
        }
    }
}
