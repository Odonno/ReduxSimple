using ReduxSimple.UnitTests.Setup.TodoListStore;
using System;
using Xunit;
using static ReduxSimple.UnitTests.Setup.TodoListStore.Functions;
using TodoListStore = ReduxSimple.ReduxStoreWithHistory<ReduxSimple.UnitTests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.UnitTests
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
                initialState
            );

            // Act
            int observeCount = 0;
            TodoListState lastState = null;

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
            Assert.Empty(lastState.TodoList);
            Assert.Equal("David", lastState.CurrentUser);
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
                initialState
            );

            // Act
            int observeCount = 0;
            TodoListState lastState = null;

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
            Assert.Empty(lastState.TodoList);
            Assert.Equal("David", lastState.CurrentUser);
        }
    }
}
