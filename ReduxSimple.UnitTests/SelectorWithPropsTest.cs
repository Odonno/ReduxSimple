using ReduxSimple.UnitTests.Setup.TodoListStore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static ReduxSimple.UnitTests.Setup.TodoListStore.Functions;
using static ReduxSimple.UnitTests.Setup.TodoListStore.Selectors;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.UnitTests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.UnitTests
{
    public class SelectorWithPropsTest
    {
        [Fact]
        public void SearchedEmptyTodoListShouldBeEmpty()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;

            store.Select(SelectSearchedItems, "no item")
                .Subscribe(items =>
                {
                    observeCount++;

                    // Assert
                    Assert.Empty(items);
                });

            // Assert
            Assert.Equal(1, observeCount);
        }

        [Fact]
        public void SearchedTodoListShouldBeEmpty()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;
            IEnumerable<TodoItem> result = null;

            store.Select(SelectSearchedItems, "no item")
                .Subscribe(items =>
                {
                    observeCount++;
                    result = items;
                });

            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");
            DispatchAddTodoItemAction(store, 2, "Create Models");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(3, observeCount);
            Assert.Empty(result);
        }

        [Fact]
        public void SearchedTodoListShouldFindResults()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );

            // Act
            int observeCount = 0;
            IEnumerable<TodoItem> result = null;

            store.Select(SelectSearchedItems, "Create")
                .Subscribe(items =>
                {
                    observeCount++;
                    result = items;
                });

            DispatchAddTodoItemAction(store, 1, "Create unit tests");
            DispatchSwitchUserAction(store, "Emily");
            DispatchAddTodoItemAction(store, 2, "Create Models");
            DispatchSwitchUserAction(store, "Emily");

            // Assert
            Assert.Equal(3, observeCount);
            Assert.Equal(2, result.Count());
        }
    }
}
