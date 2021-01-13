using Shouldly;
using Xunit;
using ReduxSimple.Tests.Setup.TodoListStore;
using System.Reactive.Linq;
using static ReduxSimple.Tests.Setup.TodoListStore.Functions;
using static ReduxSimple.Effects;
using TodoListStore = ReduxSimple.ReduxStore<ReduxSimple.Tests.Setup.TodoListStore.TodoListState>;
using System;

namespace ReduxSimple.Tests
{
    public class EffectsTest
    {
        [Fact]
        public void CanUseEffectWithDispatch()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );
            int calls = 0;

            var effectWithDispatch = CreateEffect<TodoListState>(
                () => store.ObserveAction<AddTodoItemAction>()
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

            // Assert
            store.State.TodoList?.Count.ShouldBe(2);
            calls.ShouldBe(1);
        }

        [Fact]
        public void CanUseEffectWithoutDispatch()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );
            int calls = 0;

            var effectWithoutDispatch = CreateEffect<TodoListState>(
                () => store.ObserveAction()
                    .Do(_ => calls++),
                false
            );

            store.RegisterEffects(
                effectWithoutDispatch
            );

            // Act
            DispatchAddTodoItemAction(store, 1, "Create unit tests");

            // Assert
            store.State.TodoList.ShouldHaveSingleItem();
            calls.ShouldBe(1);
        }

        [Fact]
        public void CanReplayEffectWhenExceptionOccured()
        {
            // Arrange
            var initialState = CreateInitialTodoListState();
            var store = new TodoListStore(
                Setup.TodoListStore.Reducers.CreateReducers(),
                initialState
            );
            int calls = 0;

            var effectWithDispatch = CreateEffect<TodoListState>(
                () => store.ObserveAction<AddTodoItemAction>()
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

            // Assert #1
            store.State.TodoList?.Count.ShouldBe(1);
            calls.ShouldBe(1);

            // Act #2
            DispatchAddTodoItemAction(store, 3, "Create unit tests, again");

            // Assert #2
            store.State.TodoList?.Count.ShouldBe(2);
            calls.ShouldBe(2);
        }
    }
}
