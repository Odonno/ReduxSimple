using ReduxSimple.UnitTests.Setup.TodoListStore;
using System;
using Xunit;
using static ReduxSimple.UnitTests.Setup.TodoListStore.Functions;

namespace ReduxSimple.UnitTests
{
    public class RedoTest
    {
        [Fact]
        public void CanRedo()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            DispatchAllActions(store);

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanRedo);

            store.Redo();

            Assert.True(store.CanRedo);

            store.Redo();

            // Assert
            Assert.Equal(2, store.State.TodoList.Count);
            Assert.Equal("Emily", store.State.CurrentUser);
        }

        [Fact]
        public void CannotRedo()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            DispatchAllActions(store);

            // Assert
            Assert.False(store.CanRedo);
        }
        
        [Fact]
        public void RedoAndObserveNormalTimelineActionsOnly()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            int observeCount = 0;

            store.ObserveAction(ActionOriginFilter.Normal)
                .Subscribe(_ =>
                {
                    observeCount++;
                });

            DispatchAllActions(store);

            store.Undo();
            store.Undo();
            store.Undo();
            store.Redo();
            store.Redo();

            // Assert
            Assert.Equal(4, observeCount);
        }

        [Fact]
        public void RedoAndObserveRedoneActionsOnly()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            int observeCount = 0;

            store.ObserveAction(ActionOriginFilter.Redone)
                .Subscribe(_ =>
                {
                    observeCount++;
                });

            DispatchAllActions(store);

            store.Undo();
            store.Undo();
            store.Undo();
            store.Redo();
            store.Redo();

            // Assert
            Assert.Equal(2, observeCount);
        }
    }
}
