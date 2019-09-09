using ReduxSimple.UnitTests.Setup.TodoListStore;
using Xunit;
using static ReduxSimple.UnitTests.Setup.TodoListStore.Functions;

namespace ReduxSimple.UnitTests
{
    public class DispatchTest
    {
        [Fact]
        public void CanDispatchAction()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

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
            var store = new TodoListStore(initialState);

            // Act
            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Single(store.State.TodoList);
            Assert.Equal("Emily", store.State.CurrentUser);
        }
    }
}
