using ReduxSimple.Tests.Setup.NestedArrayStore;
using Shouldly;
using System;
using Xunit;
using RootStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.NestedArrayStore.RootState>;
using static ReduxSimple.Tests.Setup.NestedArrayStore.Reducers;
using static ReduxSimple.Tests.Setup.NestedArrayStore.Selectors;

namespace ReduxSimple.Tests
{
    public class NestedArrayStoreTest
    {
        [Fact]
        public void ThrowExceptionIfUsingIndexedArrayForNestedStatesWithImplicitLens()
        {
            // Arrange
            var reducers = GetReducersFromImplicitLens();

            var store = new RootStore(
                reducers,
                RootState.InitialState
            );

            // Act
            int observeCount = 0;
            int? lastResult1 = null;
            int? lastResult2 = null;
            int? lastResult3 = null;
            int? lastResult4 = null;

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
            store.Select(SelectRandomNumber4)
                .Subscribe(number =>
                {
                    observeCount++;
                    lastResult4 = number;
                });

            var exception = Should.Throw<NotSupportedException>(() =>
                store.Dispatch(new UpdateNumberAction { Number = 10 })
            );

            // Assert
            exception.Message.ShouldBe("A sub-reducer cannot find the feature reducer of `NestedState` inside `RootState`.");
        }
        
        [Fact]
        public void ThrowExceptionIfUsingIndexedArrayForNestedStatesWithExplicitLens()
        {
            // Arrange
            var reducers = GetReducersFromExplicitLens();

            var store = new RootStore(
                reducers,
                RootState.InitialState
            );

            // Act
            int observeCount = 0;
            int? lastResult1 = null;
            int? lastResult2 = null;
            int? lastResult3 = null;
            int? lastResult4 = null;

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
            store.Select(SelectRandomNumber4)
                .Subscribe(number =>
                {
                    observeCount++;
                    lastResult4 = number;
                });

            store.Dispatch(new UpdateNumberAction { Number = 10 });

            // Assert
            observeCount.ShouldBe(5);
            lastResult1.ShouldBe(10);
            lastResult2.ShouldBe(0);
            lastResult3.ShouldBe(0);
            lastResult4.ShouldBe(0);
        }
    }
}
