using ReduxSimple.Tests.Setup.TodoListStore;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;

namespace ReduxSimple.Tests;

    public class ObserveActionTest
    {
        [Fact]
        public void CanObserveActions()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                dispatchedActionScheduler: Scheduler.Immediate
            );

            // Act
            int observeCount = 0;
            object? lastAction = null;

            store.ObserveAction()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);
            
            // Assert
            observeCount.ShouldBe(4);
            lastAction.ShouldBeOfType<AddTodoItemAction>();
        }

        [Fact]
        public void CanObserveSingleActionType()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                dispatchedActionScheduler: Scheduler.Immediate 
            );

            // Act
            int observeCount = 0;
            object? lastAction = null;

            store.ObserveAction<SwitchUserAction>()
                .Subscribe(action =>
                {
                    observeCount++;
                    lastAction = action;
                });

            DispatchAllActions(store);
            
            // Assert
            observeCount.ShouldBe(1);
            lastAction.ShouldBeOfType<SwitchUserAction>();
        }
        
        [Fact]
        public void CanObserveActionAndStateWhenDispatchedIsPassed()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState,
                dispatchedActionScheduler: Scheduler.Immediate 
            );

            TodoListState? stateBeforeAction = null;
            store.ObserveAction<AddTodoItemAction, ActionStatePair>((action, state) => new ActionStatePair
                {
                    Action = action, State = state
                })
                .Subscribe(x =>
                {
                    // Assert
                    x.State.ShouldBe(stateBeforeAction);
                });

            // Act 1
            stateBeforeAction = initialState;
            DispatchAddTodoItemAction(store, 1, "Create unit tests");

            stateBeforeAction = store.State;
            DispatchAddTodoItemAction(store, 2, "Create Models");
        }

        class ActionStatePair
        {
            public object? Action { get; set; }
            
            public object? State { get; set; }
        }
    }
