using static ReduxSimple.Selectors;

namespace ReduxSimple.Tests.Setup.ReusedStateStore
{
    public static class Selectors
    {
        public static ISelectorWithoutProps<RootState, NestedState> SelectNested1 = CreateSelector(
            (RootState state) => state.Nested1
        );
        public static ISelectorWithoutProps<RootState, NestedState> SelectNested2 = CreateSelector(
            (RootState state) => state.Nested2
        );
        public static ISelectorWithoutProps<RootState, NestedState> SelectNested3 = CreateSelector(
            (RootState state) => state.Nested3
        );

        public static ISelectorWithoutProps<RootState, int?> SelectRandomNumber1 = CreateSelector(
            SelectNested1,
            nested => nested?.RandomNumber
        );
        public static ISelectorWithoutProps<RootState, int?> SelectRandomNumber2 = CreateSelector(
            SelectNested2,
            nested => nested?.RandomNumber
        );
        public static ISelectorWithoutProps<RootState, int?> SelectRandomNumber3 = CreateSelector(
            SelectNested3,
            nested => nested?.RandomNumber
        );
    }
}
