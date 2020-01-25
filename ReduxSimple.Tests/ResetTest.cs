using ReduxSimple.Tests.Setup.TodoListStore;
using System;
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

            Assert.True(store.CanUndo);

            store.Undo();

            store.Reset();

            // Assert
            Assert.Equal(1, observeCount);
            Assert.Empty(lastState?.TodoList);
            Assert.Equal("David", lastState?.CurrentUser);
            Assert.False(store.CanRedo);
            Assert.False(store.CanUndo);
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
            Assert.Equal(1, observeCount);
            Assert.Empty(lastState?.TodoList);
            Assert.Equal("David", lastState?.CurrentUser);
        }

        [Fact]
        public void ObserveHistoryOnReset()
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

            // Assert
            Assert.Equal(5, observeCount);
            Assert.Single(lastHistory?.PreviousStates);
            Assert.IsType<InitializeStoreAction>(lastHistory?.PreviousStates[0].Action);
            Assert.Empty(lastHistory?.FutureActions);
        }
    }
}
