namespace ReduxSimple.Tests.Setup.NestedStore
{
    public class RootState
    {
        public NestedState? Nested { get; set; }

        public static RootState InitialState =>
            new RootState
            {
                Nested = NestedState.InitialState
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
}
