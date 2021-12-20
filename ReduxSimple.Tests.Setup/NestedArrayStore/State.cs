namespace ReduxSimple.Tests.Setup.NestedArrayStore;

public class RootState
{
    public ImmutableArray<NestedState> States { get; set; }

    public static RootState InitialState =>
        new RootState
        {
            States = 
                new List<NestedState>
                {
                    NestedState.InitialState,
                    NestedState.InitialState,
                    NestedState.InitialState,
                    NestedState.InitialState
                }
                .ToImmutableArray()
        };
}

public class NestedState
{
    public int? RandomNumber { get; set; }

    public static NestedState InitialState =>
        new NestedState
        {
            RandomNumber = 0
        };
}
