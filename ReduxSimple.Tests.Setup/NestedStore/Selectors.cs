using static ReduxSimple.Selectors;

namespace ReduxSimple.Tests.Setup.NestedStore;

public static class Selectors
{
    public static ISelectorWithoutProps<RootState, NestedState?> SelectNested = CreateSelector(
        (RootState state) => state.Nested
    );

    public static ISelectorWithoutProps<RootState, int?> SelectRandomNumber = CreateSelector(
        SelectNested,
        nested => nested?.RandomNumber
    );
}
