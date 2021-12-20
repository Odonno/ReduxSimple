namespace ReduxSimple.Tests.Setup.NestedStore;

public record RootState
{
    public NestedState? Nested { get; set; }

    public static RootState InitialState =>
        new RootState
        {
            Nested = NestedState.InitialState
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
