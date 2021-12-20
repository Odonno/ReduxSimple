using ReduxSimple.Tests.Setup.MultiReduceStore;
using MultiReduceStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.MultiReduceStore.MultiReduceState>;

namespace ReduxSimple.Tests;

public class ReduceTest
{
    [Fact]
    public void CanExecuteMultipleReducers()
    {
        // Arrange
        var store = new MultiReduceStore(
            Setup.MultiReduceStore.Reducers.CreateReducers()
        );

        // Act
        store.Dispatch(new UpdateNumberAction { Number = 12 });

        // Assert
        store.State.Number1.ShouldBe(12);
        store.State.Number2.ShouldBe(12);
        store.State.Number3.ShouldBe(12);
    }
}