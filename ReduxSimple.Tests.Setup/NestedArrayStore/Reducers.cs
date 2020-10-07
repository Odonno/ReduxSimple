using Converto;
using System.Collections.Generic;
using static ReduxSimple.Reducers;
using static ReduxSimple.Tests.Setup.NestedArrayStore.Selectors;

namespace ReduxSimple.Tests.Setup.NestedArrayStore
{
    public static class Reducers
    {
        public static IEnumerable<On<RootState>> GetReducersFromImplicitLens()
        {
            return CreateSubReducers(
                SelectNested1
            )
                .On<UpdateNumberAction>((state, action) => state.With(new { RandomNumber = action.Number }))
                .ToList();
        }

        public static IEnumerable<On<RootState>> GetReducersFromExplicitLens()
        {
            return CreateSubReducers(
                SelectNested1,
                (state, featureState) => state.With(new { States = state.States.SetItem(0, featureState) })
            )
                .On<UpdateNumberAction>((state, action) => state.With(new { RandomNumber = action.Number }))
                .ToList();
        }
    }
}
