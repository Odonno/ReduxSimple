using ReduxSimple.Tests.Setup.TodoListStore;
using static ReduxSimple.Selectors;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using static ReduxSimple.Tests.Setup.TodoListStore.Selectors;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests;

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
        TodoListState? lastState = null;

        store.Select()
            .Subscribe(state =>
            {
                observeCount++;
                lastState = state;
            });

        DispatchAddTodoItemAction(store, 1, "Create unit tests");
        DispatchSwitchUserAction(store, "Emily");

        // Assert
        observeCount.ShouldBe(3);
        lastState?.TodoList.ShouldHaveSingleItem();
        lastState?.CurrentUser.ShouldBe("Emily");
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
        observeCount.ShouldBe(4);
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
        ImmutableList<TodoItem>? lastResult = null;

        store.Select(SelectTodoList)
            .Subscribe(todoList =>
            {
                observeCount++;
                lastResult = todoList;
            });

        DispatchAllActions(store);

        // Assert
        observeCount.ShouldBe(4);
        lastResult?.Count.ShouldBe(3);
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
        observeCount.ShouldBe(2);
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
        (IImmutableList<TodoItem>? todoList, string? currentUser) lastPartialState = (null, null);

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
        observeCount.ShouldBe(5);
        lastPartialState.todoList?.Count.ShouldBe(3);
        lastPartialState.currentUser.ShouldBe("Emily");
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
        (IImmutableList<TodoItem>? todoList, string? currentUser) lastPartialState = (null, null);

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
        observeCount.ShouldBe(4);
        lastPartialState.todoList?.Count.ShouldBe(2);
        lastPartialState.currentUser.ShouldBe("Emily");
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
        (IImmutableList<TodoItem>? todoList, string? uselessProperty) lastPartialState = (null, null);

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
        observeCount.ShouldBe(3);
        lastPartialState.todoList?.Count.ShouldBe(2);
        lastPartialState.uselessProperty.ShouldBeNull();
    }

    [Fact]
    public void CanCombineSelectorsWithSynchronousStateUpdates()
    {
        // Arrange
        var initialState = CreateInitialTodoListState();
        var store = new TodoListStore(
            Setup.TodoListStore.Reducers.CreateReducers(),
            initialState
        );

        // Act
        int observeCount = 0;
        (IImmutableList<TodoItem>? todoList, string? currentUser) lastPartialState = (null, null);

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
        DispatchResetAction(store);

        // Assert
        observeCount.ShouldBe(6);
        lastPartialState.todoList.ShouldBeEmpty();
        lastPartialState.currentUser.ShouldBe("David");
    }
}
