using System;
using System.Collections.Immutable;
using Xunit;

namespace ReduxSimple.UnitTests
{
    public class ReduxStoreWithHistoryTest
    {
        [Fact]
        public void CanCreateAStoreWithEmptyState()
        {
            // Arrange
            var store = new HistoryStoreWithEmptyState();

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
            var store = new TodoListStoreWithHistory(initialState);

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
            var store = new TodoListStoreWithHistory(initialState);

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
            var store = new TodoListStoreWithHistory(initialState);

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
        public void CanUndoAndObserveUndoneActions()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            int observeCount = 0;
            object lastAction = null;

            store.ObserveUndoneAction()
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

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            // Assert
            Assert.Equal(2, observeCount);
            Assert.IsType<AddTodoItemAction>(lastAction);
            Assert.Single(store.State.TodoList);
            Assert.Equal("Emily", store.State.CurrentUser);
        }

        [Fact]
        public void CanUndoAndObserveUndoneActionsOfASingleType()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
            var store = new TodoListStoreWithHistory(initialState);

            // Act
            int observeCount = 0;
            object lastAction = null;

            store.ObserveUndoneAction<SwitchUserAction>()
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

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            // Assert
            Assert.Equal(1, observeCount);
            Assert.IsType<SwitchUserAction>(lastAction);
            Assert.Single(store.State.TodoList);
            Assert.Equal("David", store.State.CurrentUser);
        }

        [Fact]
        public void CanRedo()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
            var store = new TodoListStoreWithHistory(initialState);

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

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanRedo);

            store.Redo();

            Assert.True(store.CanRedo);

            store.Redo();

            // Assert
            Assert.Equal(2, store.State.TodoList.Count);
            Assert.Equal("Emily", store.State.CurrentUser);
        }

        [Fact]
        public void CannotUndo()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
            var store = new TodoListStoreWithHistory(initialState);

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

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            Assert.True(store.CanUndo);

            store.Undo();

            // Assert
            Assert.False(store.CanUndo);
        }

        [Fact]
        public void CannotRedo()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
            var store = new TodoListStoreWithHistory(initialState);

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
            Assert.False(store.CanRedo);
        }

        [Fact]
        public void CanResetStoreWithUndoneActions()
        {
            // Arrange
            var initialState = new TodoListState
            {
                TodoList = ImmutableList.Create<TodoItem>(),
                CurrentUser = "David"
            };
            var store = new TodoListStoreWithHistory(initialState);

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

            Assert.True(store.CanUndo);

            store.Undo();

            store.Reset();

            // Assert
            Assert.Equal(1, observeCount);
            Assert.Empty(lastState.TodoList);
            Assert.Equal("David", lastState.CurrentUser);
            Assert.False(store.CanRedo);
            Assert.False(store.CanUndo);
        }
    }
}
