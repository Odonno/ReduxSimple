using ReduxSimple.UnitTests.Setup.EmptyStore;
using ReduxSimple.UnitTests.Setup.TodoListStore;
using Xunit;
using static ReduxSimple.UnitTests.Setup.TodoListStore.Functions;

namespace ReduxSimple.UnitTests
{
    public class CreateReduxStoreWithHistoryTest
    {
        [Fact]
        public void CanCreateAStoreWithEmptyState()
        {
            // Arrange
            var store = new ReduxStoreWithHistory<EmptyState>(
                Setup.EmptyStore.Reducers.CreateReducers()
            );

            // Act

            // Assert
            Assert.NotNull(store.State);
        }

        [Fact]
        public void CanCreateAStoreWithDefaultState()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new ReduxStoreWithHistory<TodoListState>(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act

            // Assert
            Assert.Empty(store.State.TodoList);
        }
    }
}
