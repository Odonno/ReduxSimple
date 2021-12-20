using static ReduxSimple.Selectors;

namespace ReduxSimple.Tests.Setup.NestedArrayStore;

public static class Selectors
{
    public static ISelectorWithoutProps<RootState, NestedState> SelectNested1 = CreateSelector(
        (RootState state) => state.States[0]
    );
    public static ISelectorWithoutProps<RootState, NestedState> SelectNested2 = CreateSelector(
        (RootState state) => state.States[1]
    );
    public static ISelectorWithoutProps<RootState, NestedState> SelectNested3 = CreateSelector(
        (RootState state) => state.States[2]
    );
    public static ISelectorWithoutProps<RootState, NestedState> SelectNested4 = CreateSelector(
        (RootState state) => state.States[3]
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
    public static ISelectorWithoutProps<RootState, int?> SelectRandomNumber4 = CreateSelector(
        SelectNested4,
        nested => nested?.RandomNumber
    );
}
