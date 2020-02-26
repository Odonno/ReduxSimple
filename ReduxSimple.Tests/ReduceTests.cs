using ReduxSimple.Tests.Setup.MultiReduceStore;
using Xunit;
using MultiReduceStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.MultiReduceStore.MultiReduceState>;

namespace ReduxSimple.Tests
{
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
            Assert.Equal(12, store.State.Number1);
            Assert.Equal(12, store.State.Number2);
            Assert.Equal(12, store.State.Number3);
        }
    }
}
