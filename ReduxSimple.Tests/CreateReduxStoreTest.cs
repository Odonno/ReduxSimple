using Shouldly;
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
            store.State.ShouldNotBeNull();
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
            store.State.TodoList.ShouldBeEmpty();
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
            var previousState = history.PreviousStates.ShouldHaveSingleItem();
            previousState.Action.ShouldBeOfType<InitializeStoreAction>();
            history.FutureActions.ShouldBeEmpty();
        }
    }
}
