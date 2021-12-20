using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests;

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

    [Fact]
    public void CanDispatchActionsConcurrently()
    {
        // Arrange
        var initialState = CreateInitialTodoListState();
        var store = new TodoListStore(
            Setup.TodoListStore.Reducers.CreateReducers(),
            initialState
        );

        int parallelCount = 1000;

        // Act
        Parallel.For(0, parallelCount, i =>
        {
            int taskId = i + 1;
            DispatchAddTodoItemAction(store, taskId, $"Task {taskId}");
        });

        // Assert
        store.State.TodoList.ShouldNotBeNull();
        if (store.State.TodoList != null)
        {
            store.State.TodoList.Count.ShouldBe(parallelCount);
        }
    }
}

