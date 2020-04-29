using ReduxSimple.Tests.Setup.TodoListStore;
using Shouldly;
using System;
using Xunit;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests
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
            result.ShouldBeFalse();
            store.State.CurrentUser.ShouldBe("David");
            store.State.TodoList.ShouldBeEmpty();
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
            object? lastAction = null;

            store.ObserveUndoneAction()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            // Assert
            observeCount.ShouldBe(2);
            lastAction.ShouldBeOfType<AddTodoItemAction>();
            store.State.TodoList.ShouldHaveSingleItem();
            store.State.CurrentUser.ShouldBe("Emily");
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
            object? lastAction = null;

            store.ObserveUndoneAction<SwitchUserAction>()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            // Assert
            observeCount.ShouldBe(1);
            lastAction.ShouldBeOfType<SwitchUserAction>();
            store.State.TodoList.ShouldHaveSingleItem();
            store.State.CurrentUser.ShouldBe("David");
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

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            store.CanUndo.ShouldBeTrue();

            store.Undo();

            // Assert
            store.CanUndo.ShouldBeFalse();
            store.State.CurrentUser.ShouldBe("David");
            store.State.TodoList.ShouldBeEmpty();
        }
    }
}
