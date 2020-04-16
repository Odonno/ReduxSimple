using ReduxSimple.Tests.Setup.ReusedStateStore;
using System;
using System.Linq;
using Xunit;
using RootStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.ReusedStateStore.RootState>;
using static ReduxSimple.Reducers;
using static ReduxSimple.Tests.Setup.ReusedStateStore.Reducers;
using static ReduxSimple.Tests.Setup.ReusedStateStore.Selectors;

namespace ReduxSimple.Tests
{
    public class ReusedStateStoreTest
    {
        [Fact]
        public void CanCreateAndUseSubReducers()
        {
            // Arrange
            var nestedReducers = CreateReducers();

            var reducers2 = CreateSubReducers(
                nestedReducers.ToArray(),
                SelectNested2
            );
            var reducers1 = CreateSubReducers(
                nestedReducers.ToArray(),
                SelectNested1
            );
            var reducers3 = CreateSubReducers(
                nestedReducers.ToArray(),
                SelectNested3
            );

            var reducers = reducers1.Concat(reducers2).Concat(reducers3);

            var store = new RootStore(
                reducers,
                RootState.InitialState
            );

            // Act
            int observeCount = 0;
            int? lastResult1 = null;
            int? lastResult2 = null;
            int? lastResult3 = null;

            store.Select(SelectRandomNumber1)
                .Subscribe(number =>
                {
                    observeCount++;
                    lastResult1 = number;
                });
            store.Select(SelectRandomNumber2)
                .Subscribe(number =>
                {
                    observeCount++;
                    lastResult2 = number;
                });
            store.Select(SelectRandomNumber3)
                .Subscribe(number =>
                {
                    observeCount++;
                    lastResult3 = number;
                });

            store.Dispatch(new UpdateNumberAction { Number = 10 });

            // Assert
            Assert.Equal(6, observeCount);
            Assert.Equal(10, lastResult1);
            Assert.Equal(10, lastResult2);
            Assert.Equal(10, lastResult3);
        }
    }
}
