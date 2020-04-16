using Converto;
using System.Collections.Generic;
using static ReduxSimple.Reducers;

namespace ReduxSimple.Tests.Setup.ReusedStateStore
{
    public static class Reducers
    {
        public static IEnumerable<On<NestedState>> CreateReducers()
        {
            return new List<On<NestedState>>
            {
                On<UpdateNumberAction, NestedState>(
                    (state, action) => state.With(new { RandomNumber = action.Number })
                )
            };
        }
    }
}
