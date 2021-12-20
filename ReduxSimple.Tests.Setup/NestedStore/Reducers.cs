using static ReduxSimple.Reducers;

namespace ReduxSimple.Tests.Setup.NestedStore;

public static class Reducers
{
    public static IEnumerable<On<NestedState>> CreateReducers()
    {
        return new List<On<NestedState>>
        {
            On<UpdateNumberAction, NestedState>(
                (state, action) => state with { RandomNumber = action.Number }
            )
        };
    }
}
