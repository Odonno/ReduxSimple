using ReduxSimple.Tests.Setup.TodoListStore;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests
{
    public class ResetTest
    {
        [Fact]
        public void CanResetStoreWithUndoneActions()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                true
            );

            // Act
            int observeCount = 0;
            TodoListState? lastState = null;

            store.ObserveReset()
                .Subscribe(state =>
                {
                    observeCount++;
                    lastState = state;
                });

            DispatchAllActions(store);

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            store.Reset();

            // Assert
            observeCount.ShouldBe(1);
            lastState?.TodoList.ShouldBeEmpty();
            lastState?.CurrentUser.ShouldBe("David");
            store.CanRedo.ShouldBeFalse();
            store.CanUndo.ShouldBeFalse();
        }

        [Fact]
        public void CanResetStore()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                true
            );

            // Act
            int observeCount = 0;
            TodoListState? lastState = null;

            store.ObserveReset()
                .Subscribe(state =>
                {
                    observeCount++;
                    lastState = state;
                });

            DispatchAllActions(store);

            store.Reset();

            // Assert
            observeCount.ShouldBe(1);
            lastState?.TodoList.ShouldBeEmpty();
            lastState?.CurrentUser.ShouldBe("David");
        }

        [Fact]
        public async Task ObserveHistoryOnReset()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                true
            );

            // Act
            int observeCount = 0;
            ReduxHistory<TodoListState>? lastHistory = null;

            store.ObserveHistory()
                .Subscribe(history =>
                {
                    observeCount++;
                    lastHistory = history;
                });

            DispatchAllActions(store);

            store.Reset();

            await Task.Delay(100);

            // Assert
            observeCount.ShouldBe(5);
            var previousState = lastHistory?.PreviousStates.ShouldHaveSingleItem();
            previousState?.Action.ShouldBeOfType<InitializeStoreAction>();
            lastHistory?.FutureActions.ShouldBeEmpty();
        }
    }
}
