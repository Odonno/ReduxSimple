using ReduxSimple.UnitTests.Setup.TodoListStore;
using System;
using Xunit;
using static ReduxSimple.UnitTests.Setup.TodoListStore.Functions;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.UnitTests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.UnitTests
{
    public class UndoTest
    {
        [Fact]
        public void CannotUndoIfNoActionDispatched()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                true
            );

            // Act
            bool result = store.CanUndo;

            // Assert
            Assert.False(result);
            Assert.Equal("David", store.State.CurrentUser);
            Assert.Empty(store.State.TodoList);
        }

        [Fact]
        public void CanUndoAndObserveUndoneActions()
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
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                true
            );

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
        public void CannotUndo()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                true
            );

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
            Assert.Equal("David", store.State.CurrentUser);
            Assert.Empty(store.State.TodoList);
        }
    }
}
