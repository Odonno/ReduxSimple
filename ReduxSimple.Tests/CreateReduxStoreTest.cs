using Xunit;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using EmptyStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.EmptyStore.EmptyState>;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests
{
    public class CreateReduxStoreTest
    {
        [Fact]
        public void CanCreateAStoreWithEmptyState()
        {
            // Arrange
            var store = new EmptyStore(
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
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act

            // Assert
            Assert.Empty(store.State.TodoList);
        }

        [Fact]
        public void CreatingAStoreShouldDispatchInitializeAction()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                true
            );

            // Act
            var history = store.GetHistory();

            // Assert
            Assert.Single(history.PreviousStates);
            Assert.IsType<InitializeStoreAction>(history.PreviousStates[0].Action);
            Assert.Empty(history.FutureActions);
        }
    }
}
