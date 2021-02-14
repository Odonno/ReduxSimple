using Shouldly;
using Xunit;
using ReduxSimple.Tests.Setup.TodoListStore;
using System.Reactive.Linq;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using static ReduxSimple.Effects;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;
using System;
using Microsoft.Reactive.Testing;
using static Microsoft.Reactive.Testing.ReactiveTest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReduxSimple.Tests
{
    public class EffectsTest
    {
        [Fact]
        public async Task CanUseEffectWithDispatch()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );
            int calls = 0;

            var effectWithDispatch = CreateEffect<TodoListState>(
                (s) => s.ObserveAction<AddTodoItemAction>()
                    .Where(action => action.TodoItem?.Id == 1)
                    .Do(_ => calls++)
                    .Select(_ =>
                    {
                        return new AddTodoItemAction
                        {
                            TodoItem = new TodoItem
                            {
                                Id = 2,
                                Title = "Listen to side effects"
                            }
                        };
                    }),
                true
            );

            store.RegisterEffects(
                effectWithDispatch
            );

            // Act
            DispatchAddTodoItemAction(store, 1, "Create unit tests");

            await Task.Delay(100);

            // Assert
            store.State.TodoList?.Count.ShouldBe(2);
            calls.ShouldBe(1);
        }

        [Fact]
        public async Task CanUseEffectWithoutDispatch()
        {
            // Arrange
            var scheduler = new TestScheduler();

            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );
            int calls = 0;

            var effectWithoutDispatch = CreateEffect<TodoListState>(
                (s) => s.ObserveAction()
                    .Do(_ => calls++),
                false
            );

            store.RegisterEffects(
                effectWithoutDispatch
            );

            // Act
            var actions = scheduler.CreateHotObservable(
                OnNext(100, new AddTodoItemAction
                {
                    TodoItem = new TodoItem
                    {
                        Id = 1,
                        Title = "Create unit tests"
                    }
                })
            );

            actions.Subscribe(store.Dispatch);

            scheduler.Start();
            scheduler.AdvanceBy(200);

            await Task.Delay(100);

            // Assert
            store.State.TodoList.ShouldHaveSingleItem();
            calls.ShouldBe(1);
        }

        [Fact]
        public async Task CanReplayEffectWhenExceptionOccured()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );
            int calls = 0;

            var effectWithDispatch = CreateEffect<TodoListState>(
                (s) => s.ObserveAction<AddTodoItemAction>()
                    .Do(_ => calls++)
                    .Select(_ =>
                    {
                        throw new Exception("Too bad!");

#pragma warning disable CS0162 // Unreachable code detected
                        return new AddTodoItemAction
                        {
                            TodoItem = new TodoItem
                            {
                                Id = 2,
                                Title = "Listen to side effects"
                            }
                        };
#pragma warning restore CS0162 // Unreachable code detected
                    }),
                true
            );

            store.RegisterEffects(
                effectWithDispatch
            );

            // Act #1
            DispatchAddTodoItemAction(store, 1, "Create unit tests");

            await Task.Delay(100);

            // Assert #1
            store.State.TodoList?.Count.ShouldBe(1);
            calls.ShouldBe(1);

            // Act #2
            DispatchAddTodoItemAction(store, 3, "Create unit tests, again");

            await Task.Delay(100);

            // Assert #2
            store.State.TodoList?.Count.ShouldBe(2);
            calls.ShouldBe(2);
        }
    
        [Fact]
        public async Task SynchronousEffectsShouldSubscribeInOrder()
        {
            // Arrange
            var scheduler = new TestScheduler();

            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );
            int callIndex = 0;

            var effect1Dispatch = CreateEffect<TodoListState>(
                (s) => s.ObserveAction<AddTodoItemAction>()
                    .Where(action => action.TodoItem?.Id == 1)
                    .Select(_ =>
                    {
                        return new AddTodoItemAction
                        {
                            TodoItem = new TodoItem
                            {
                                Id = 2,
                                Title = "Listen to side effects"
                            }
                        };
                    }),
                true
            );

            var actionsObserved = new List<AddTodoItemAction?>();

            var effect2Dispatch = CreateEffect<TodoListState>(
                (s) => s.ObserveAction()
                    .Do(action =>
                    {
                        var addTodoItemAction = action as AddTodoItemAction;
                        actionsObserved.Add(addTodoItemAction);

                        callIndex++;
                    }),
                false
            );


            store.RegisterEffects(
                effect1Dispatch,
                effect2Dispatch
            );

            // Act
            var actions = scheduler.CreateHotObservable(
                OnNext(100, new AddTodoItemAction
                {
                    TodoItem = new TodoItem
                    {
                        Id = 1,
                        Title = "Create unit tests"
                    }
                })
            );

            actions.Subscribe(store.Dispatch);

            scheduler.Start();
            scheduler.AdvanceBy(200);

            await Task.Delay(100);

            // Assert
            store.State.TodoList?.Count.ShouldBe(2);

            actionsObserved.Count.ShouldBe(2);

            actionsObserved[0].ShouldNotBeNull();
            actionsObserved[0]?.TodoItem?.Id.ShouldBe(1);

            actionsObserved[1].ShouldNotBeNull();
            actionsObserved[1]?.TodoItem?.Id.ShouldBe(2);
        }
    }
}
