using ReduxSimple.Tests.Setup.TodoListStore;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using static ReduxSimple.Tests.Setup.TodoListStore.Selectors;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests;

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
                items.ShouldBeEmpty();
            });

        // Assert
        observeCount.ShouldBe(1);
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
        IEnumerable<TodoItem>? result = null;

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
        observeCount.ShouldBe(3);
        result.ShouldBeEmpty();
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
        IEnumerable<TodoItem>? result = null;

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
        observeCount.ShouldBe(3);
        result.Count().ShouldBe(2);
    }
}
