using static ReduxSimple.Reducers;

namespace ReduxSimple.Tests.Setup.MultiReduceStore;

public static class Reducers
{
    public static IEnumerable<On<MultiReduceState>> CreateReducers()
    {
        return new List<On<MultiReduceState>>
        {
            On<UpdateNumberAction, MultiReduceState>(
                (state, action) => state with { Number1 = action.Number }
            ),
            On<UpdateNumberAction, MultiReduceState>(
                (state, action) => state with { Number2 = action.Number }
            ),
            On<UpdateNumberAction, MultiReduceState>(
                (state, action) => state with { Number3 = action.Number }
            )
        };
    }
}
