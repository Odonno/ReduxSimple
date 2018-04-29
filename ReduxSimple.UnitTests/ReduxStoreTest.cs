using System;
using System.Collections.Immutable;
using Xunit;

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
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>()
            };
            var store = new TodoListStore(initialState);

            // Act

            // Assert
            Assert.Empty(store.State.TodoList);
        }
        
        [Fact]
        public void CanDispatchAction()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>()
            };
            var store = new TodoListStore(initialState);

            // Act
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 1,
                    Title = "Create unit tests"
                }
            });

            // Assert
            Assert.Single(store.State.TodoList);
        }

        [Fact]
        public void CanDispatchDifferentActions()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
            var store = new TodoListStore(initialState);

            // Act
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 1,
                    Title = "Create unit tests"
                }
            });
            store.Dispatch(new SwitchUserAction
            {
                NewUser = "Emily"
            });

            // Assert
            Assert.Single(store.State.TodoList);
            Assert.Equal("Emily", store.State.CurrentUser);
        }

        [Fact]
        public void CanObserveEntireState()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
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

            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 1,
                    Title = "Create unit tests"
                }
            });
            store.Dispatch(new SwitchUserAction
            {
                NewUser = "Emily"
            });

            // Assert
            Assert.Equal(2, observeCount);
            Assert.Single(lastState.TodoList);
            Assert.Equal("Emily", lastState.CurrentUser);
        }

        [Fact]
        public void CanObserveOnePropertyOfState()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
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

            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 1,
                    Title = "Create unit tests"
                }
            });
            store.Dispatch(new SwitchUserAction
            {
                NewUser = "Emily"
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 2,
                    Title = "Create Models"
                }
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 3,
                    Title = "Refactor tests"
                }
            });

            // Assert
            Assert.Equal(3, observeCount);
            Assert.Equal(3, lastResult.Count);
        }

        [Fact]
        public void CanObservePartialStateWithTwoProperties()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
            var store = new TodoListStore(initialState);

            // Act
            int observeCount = 0;
            dynamic lastPartialState = null;

            store.ObserveState(state => new { state.TodoList, state.CurrentUser })
                .Subscribe(partialState =>
                {
                    observeCount++;
                    lastPartialState = partialState;
                });

            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 1,
                    Title = "Create unit tests"
                }
            });
            store.Dispatch(new SwitchUserAction
            {
                NewUser = "Emily"
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 2,
                    Title = "Create Models"
                }
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 3,
                    Title = "Refactor tests"
                }
            });

            // Assert
            Assert.Equal(4, observeCount);
            Assert.Equal(3, lastPartialState.TodoList.Count);
            Assert.Equal("Emily", lastPartialState.CurrentUser);
        }

        [Fact]
        public void CanObserveActions()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
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

            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 1,
                    Title = "Create unit tests"
                }
            });
            store.Dispatch(new SwitchUserAction
            {
                NewUser = "Emily"
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 2,
                    Title = "Create Models"
                }
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 3,
                    Title = "Refactor tests"
                }
            });

            // Assert
            Assert.Equal(4, observeCount);
            Assert.IsType<AddTodoItemAction>(lastAction);
        }

        [Fact]
        public void CanObserveSingleActionType()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
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

            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 1,
                    Title = "Create unit tests"
                }
            });
            store.Dispatch(new SwitchUserAction
            {
                NewUser = "Emily"
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 2,
                    Title = "Create Models"
                }
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 3,
                    Title = "Refactor tests"
                }
            });

            // Assert
            Assert.Equal(1, observeCount);
            Assert.IsType<SwitchUserAction>(lastAction);
        }

        [Fact]
        public void CanResetStore()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
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

            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 1,
                    Title = "Create unit tests"
                }
            });
            store.Dispatch(new SwitchUserAction
            {
                NewUser = "Emily"
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 2,
                    Title = "Create Models"
                }
            });
            store.Dispatch(new AddTodoItemAction
            {
                TodoItem = new TodoItem
                {
                    Id = 3,
                    Title = "Refactor tests"
                }
            });

            store.Reset();

            // Assert
            Assert.Equal(1, observeCount);
            Assert.Empty(lastState.TodoList);
            Assert.Equal("David", lastState.CurrentUser);
        }
    }
}
