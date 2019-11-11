using ReduxSimple.Tests.Setup.TodoListStore;
using System;
using System.Collections.Immutable;
using System.Reactive.Linq;
using Xunit;
using static ReduxSimple.Selectors;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using static ReduxSimple.Tests.Setup.TodoListStore.Selectors;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests
{
    public class SelectorTest
    {
        [Fact]
        public void CanSelectEntireState()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;
            TodoListState lastState = null;

            store.Select()
                .Subscribe(state =>
                {
                    observeCount++;
                    lastState = state;
                });

            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(3, observeCount);
            Assert.Single(lastState.TodoList);
            Assert.Equal("Emily", lastState.CurrentUser);
        }

        [Fact]
        public void CanSelectEntireStateWithUnchangedValue()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;

            store.Select()
                .Subscribe(state =>
                {
                    observeCount++;
                });

            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchAddTodoItemAction(store, 2, "Create Models");
            DispatchSwitchUserAction(store, "Emily");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(4, observeCount);
        }

        [Fact]
        public void CanSelectOnePropertyOfState()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;
            ImmutableList<TodoItem> lastResult = null;

            store.Select(SelectTodoList)
                .Subscribe(todoList =>
                {
                    observeCount++;
                    lastResult = todoList;
                });

            DispatchAllActions(store);

            // Assert
            Assert.Equal(4, observeCount);
            Assert.Equal(3, lastResult.Count);
        }

        [Fact]
        public void CanSelectOnePropertyOfStateWithUnchangedValue()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;

            store.Select(SelectCurrentUser)
                .Subscribe(_ =>
                {
                    observeCount++;
                });

            DispatchSwitchUserAction(store, "Emily");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(2, observeCount);
        }

        [Fact]
        public void CanCombineSelectors()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;
            (IImmutableList<TodoItem> todoList, string currentUser) lastPartialState = (null, null);

            store.Select(
                CombineSelectors(SelectTodoList, SelectCurrentUser)
            )
                .Subscribe(x =>
                {
                    var (todolist, currentUser) = x;

                    observeCount++;
                    lastPartialState = (todolist, currentUser);
                });

            DispatchAllActions(store);

            // Assert
            Assert.Equal(5, observeCount);
            Assert.Equal(3, lastPartialState.todoList.Count);
            Assert.Equal("Emily", lastPartialState.currentUser);
        }

        [Fact]
        public void CanCombineSelectorsWithUnchangedValue()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;
            (IImmutableList<TodoItem> todoList, string currentUser) lastPartialState = (null, null);

            store.Select(
                CombineSelectors(SelectTodoList, SelectCurrentUser)
            )
                .Subscribe(x =>
                {
                    var (todolist, currentUser) = x;

                    observeCount++;
                    lastPartialState = (todolist, currentUser);
                });

            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");
            DispatchAddTodoItemAction(store, 2, "Create Models");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(4, observeCount);
            Assert.Equal(2, lastPartialState.todoList.Count);
            Assert.Equal("Emily", lastPartialState.currentUser);
        }

        [Fact]
        public void CanCombineSelectorsWithOneUpdatedPropertyAndOneNonUpdateProperty()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;
            (IImmutableList<TodoItem> todoList, string uselessProperty) lastPartialState = (null, null);

            store.Select(
                CombineSelectors(SelectTodoList, SelectUselessProperty)
            )
                .Subscribe(x =>
                {
                    var (todolist, uselessProperty) = x;

                    observeCount++;
                    lastPartialState = (todolist, uselessProperty);
                });

            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");
            DispatchAddTodoItemAction(store, 2, "Create Models");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(3, observeCount);
            Assert.Equal(2, lastPartialState.todoList.Count);
            Assert.Null(lastPartialState.uselessProperty);
        }
    }
}
