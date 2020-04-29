using Shouldly;
using Xunit;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests
{
    public class DispatchTest
    {
        [Fact]
        public void CanDispatchAction()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            DispatchAddTodoItemAction(store, 1, "Create unit tests");

            // Assert
            store.State.TodoList.ShouldHaveSingleItem();
        }

        [Fact]
        public void CanDispatchDifferentActions()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(), 
                initialState
            );

            // Act
            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            store.State.TodoList.ShouldHaveSingleItem();
            store.State.CurrentUser.ShouldBe("Emily");
        }
    }
}
