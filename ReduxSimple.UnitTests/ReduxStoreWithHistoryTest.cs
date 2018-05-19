using System;
using Xunit;
using static ReduxSimple.UnitTests.Functions;

namespace ReduxSimple.UnitTests
{
    public class ReduxStoreWithHistoryTest
    {
        [Fact]
        public void CanCreateAStoreWithEmptyState()
        {
            // Arrange
            var store = new HistoryStoreWithEmptyState();

            // Act

            // Assert
            Assert.NotNull(store.State);
        }

        [Fact]
        public void CanCreateAStoreWithDefaultState()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

            // Act

            // Assert
            Assert.Empty(store.State.TodoList);
        }

        [Fact]
        public void CanDispatchAction()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            DispatchAddTodoItemAction(store, 1, "Create unit tests");

            // Assert
            Assert.Single(store.State.TodoList);
        }

        [Fact]
        public void CanDispatchDifferentActions()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Single(store.State.TodoList);
            Assert.Equal("Emily", store.State.CurrentUser);
        }

        [Fact]
        public void CanUndoAndObserveUndoneActions()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            int observeCount = 0;
            object lastAction = null;

            store.ObserveUndoneAction()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            // Assert
            Assert.Equal(2, observeCount);
            Assert.IsType<AddTodoItemAction>(lastAction);
            Assert.Single(store.State.TodoList);
            Assert.Equal("Emily", store.State.CurrentUser);
        }

        [Fact]
        public void CanUndoAndObserveUndoneActionsOfASingleType()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            int observeCount = 0;
            object lastAction = null;

            store.ObserveUndoneAction<SwitchUserAction>()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            // Assert
            Assert.Equal(1, observeCount);
            Assert.IsType<SwitchUserAction>(lastAction);
            Assert.Single(store.State.TodoList);
            Assert.Equal("David", store.State.CurrentUser);
        }

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
        public void CannotUndo()
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

            Assert.True(store.CanUndo);

            store.Undo();

            // Assert
            Assert.False(store.CanUndo);
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
        public void CanResetStoreWithUndoneActions()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStoreWithHistory(initialState);

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
