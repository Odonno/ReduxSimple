using System;
using System.Collections.Immutable;
using Xunit;
using static ReduxSimple.UnitTests.Functions;

namespace ReduxSimple.UnitTests
{
    public class ReduxStoreTest
    {
        [Fact]
        public void CanCreateAStoreWithEmptyState()
        {
            // Arrange
            var store = new StoreWithEmptyState();

            // Act

            // Assert
            Assert.NotNull(store.State);
        }

        [Fact]
        public void CanCreateAStoreWithDefaultState()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

            // Act

            // Assert
            Assert.Empty(store.State.TodoList);
        }
        
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

        [Fact]
        public void CanObserveEntireState()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

            // Act
            int observeCount = 0;
            TodoListState lastState = null;

            store.ObserveState()
                .Subscribe(state =>
                {
                    observeCount++;
                    lastState = state;
                });

            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(2, observeCount);
            Assert.Single(lastState.TodoList);
            Assert.Equal("Emily", lastState.CurrentUser);
        }

        [Fact]
        public void CanObserveEntireStateWithUnchangedValue()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

            // Act
            int observeCount = 0;

            store.ObserveState()
                .Subscribe(state =>
                {
                    observeCount++;
                });

            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchAddTodoItemAction(store, 2, "Create Models");
            DispatchSwitchUserAction(store, "Emily");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(3, observeCount);
        }

        [Fact]
        public void CanObserveOnePropertyOfState()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

            // Act
            int observeCount = 0;
            ImmutableList<TodoItem> lastResult = null;

            store.ObserveState(state => state.TodoList)
                .Subscribe(todoList =>
                {
                    observeCount++;
                    lastResult = todoList;
                });

            DispatchAllActions(store);

            // Assert
            Assert.Equal(3, observeCount);
            Assert.Equal(3, lastResult.Count);
        }

        [Fact]
        public void CanObserveOnePropertyOfStateWithUnchangedValue()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

            // Act
            int observeCount = 0;

            store.ObserveState(state => state.CurrentUser)
                .Subscribe(_ =>
                {
                    observeCount++;
                });

            DispatchSwitchUserAction(store, "Emily");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(1, observeCount);
        }

        [Fact]
        public void CanObservePartialStateWithTwoProperties()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

            // Act
            int observeCount = 0;
            (IImmutableList<TodoItem> todoList, string currentUser) lastPartialState = (null, null);

            store.ObserveState(state => (state.TodoList, state.CurrentUser))
                .Subscribe(partialState =>
                {
                    observeCount++;
                    lastPartialState = partialState;
                });

            DispatchAllActions(store);

            // Assert
            Assert.Equal(4, observeCount);
            Assert.Equal(3, lastPartialState.todoList.Count);
            Assert.Equal("Emily", lastPartialState.currentUser);
        }

        [Fact]
        public void CanObservePartialStateWithTwoPropertiesWithUnchangedValue()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

            // Act
            int observeCount = 0;
            (IImmutableList<TodoItem> todoList, string currentUser) lastPartialState = (null, null);

            store.ObserveState(state => (state.TodoList, state.CurrentUser))
                .Subscribe(partialState =>
                {
                    observeCount++;
                    lastPartialState = partialState;
                });

            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");
            DispatchAddTodoItemAction(store, 2, "Create Models");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(3, observeCount);
            Assert.Equal(2, lastPartialState.todoList.Count);
            Assert.Equal("Emily", lastPartialState.currentUser);
        }

        [Fact]
        public void CanObserveActions()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

            // Act
            int observeCount = 0;
            object lastAction = null;

            store.ObserveAction()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);

            // Assert
            Assert.Equal(4, observeCount);
            Assert.IsType<AddTodoItemAction>(lastAction);
        }

        [Fact]
        public void CanObserveSingleActionType()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

            // Act
            int observeCount = 0;
            object lastAction = null;

            store.ObserveAction<SwitchUserAction>()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);

            // Assert
            Assert.Equal(1, observeCount);
            Assert.IsType<SwitchUserAction>(lastAction);
        }

        [Fact]
        public void CanResetStore()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(initialState);

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
