namespace ReduxSimple.Tests.Setup.NestedArrayStore;

public record RootState
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

public record NestedState
{
    public int? RandomNumber { get; set; }

    public static NestedState InitialState =>
        new NestedState
        {
            RandomNumber = 0
        };
}
