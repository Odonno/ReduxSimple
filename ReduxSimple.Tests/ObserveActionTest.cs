using ReduxSimple.Tests.Setup.TodoListStore;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests
{
    public class ObserveActionTest
    {
        [Fact]
        public async Task CanObserveActions()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;
            object? lastAction = null;

            store.ObserveAction()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);

            await Task.Delay(100);

            // Assert
            observeCount.ShouldBe(4);
            lastAction.ShouldBeOfType<AddTodoItemAction>();
        }

        [Fact]
        public async Task CanObserveSingleActionType()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;
            object? lastAction = null;

            store.ObserveAction<SwitchUserAction>()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);

            await Task.Delay(100);

            // Assert
            observeCount.ShouldBe(1);
            lastAction.ShouldBeOfType<SwitchUserAction>();
        }
    }
}
