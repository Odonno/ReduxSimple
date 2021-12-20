namespace ReduxSimple.Tests.Setup.ReusedStateStore;

public record RootState
{
    public NestedState Nested1 { get; set; } = NestedState.InitialState;
    public NestedState Nested2 { get; set; } = NestedState.InitialState;
    public NestedState Nested3 { get; set; } = NestedState.InitialState;

    public static RootState InitialState =>
        new RootState
        {
            Nested1 = NestedState.InitialState,
            Nested2 = NestedState.InitialState,
            Nested3 = NestedState.InitialState
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
