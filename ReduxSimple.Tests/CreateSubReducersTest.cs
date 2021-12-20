using ReduxSimple.Tests.Setup.NestedStore;
using RootStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.NestedStore.RootState>;
using static ReduxSimple.Reducers;
using static ReduxSimple.Tests.Setup.NestedStore.Reducers;
using static ReduxSimple.Tests.Setup.NestedStore.Selectors;

namespace ReduxSimple.Tests;

public class CreateSubReducersTest
{
    [Fact]
    public void CanCreateAndUseSubReducers()
    {
        // Arrange
        var nestedReducers = CreateReducers();
        var reducers = CreateSubReducers(
            nestedReducers.ToArray(),
            SelectNested
        );

        var store = new RootStore(
            reducers,
            RootState.InitialState
        );

        // Act
        int observeCount = 0;
        int? lastResult = null;

        store.Select(SelectRandomNumber)
            .Subscribe(number =>
            {
                observeCount++;
                lastResult = number;
            });
                
        store.Dispatch(new UpdateNumberAction { Number = 10 });

        // Assert
        observeCount.ShouldBe(2);
        lastResult.ShouldBe(10);
    }
}
