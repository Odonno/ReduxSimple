using ReduxSimple.Tests.Setup.NestedStore;
using System;
using System.Linq;
using Xunit;
using RootStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.NestedStore.RootState>;
using static ReduxSimple.Reducers;
using static ReduxSimple.Tests.Setup.NestedStore.Reducers;
using static ReduxSimple.Tests.Setup.NestedStore.Selectors;

namespace ReduxSimple.Tests
{
    public class CreateSubReducersTests
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
            Assert.Equal(2, observeCount);
            Assert.Equal(10, lastResult);
        }
    }
}
