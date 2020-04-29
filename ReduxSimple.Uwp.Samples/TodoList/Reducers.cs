using Converto;
using System.Collections.Generic;
using System.Linq;
using static ReduxSimple.Reducers;
using static ReduxSimple.Uwp.Samples.TodoList.Selectors;
using static ReduxSimple.Uwp.Samples.TodoList.Entities;

namespace ReduxSimple.Uwp.Samples.TodoList
{
    public static class Reducers
    {
        public static IEnumerable<On<RootState>> GetReducers()
        {
            return CreateSubReducers(SelectTodoListState)
                .On<SetFilterAction>((state, action) => state.With(new { action.Filter }))
                .On<CreateTodoItemAction>(
                    state =>
                    {
                        int newId = state.Items.Collection.Any() ? state.Items.Ids.Max() + 1 : 1;
                        return state.With(new
                        {
                            Items = TodoItemAdapter.UpsertOne(new TodoItem { Id = newId }, state.Items)
                        });
                    }
                )
                .On<CompleteTodoItemAction>(
                    (state, action) =>
                    {
                        return state.With(new
                        {
                            Items = TodoItemAdapter.UpsertOne(new { action.Id, Completed = true }, state.Items)
                        });
                    }
                )
                .On<RevertCompleteTodoItemAction>(
                    (state, action) =>
                    {
                        return state.With(new
                        {
                            Items = TodoItemAdapter.UpsertOne(new { action.Id, Completed = false }, state.Items)
                        });
                    }
                )
                .On<UpdateTodoItemAction>(
                    (state, action) =>
                    {
                        return state.With(new
                        {
                            Items = TodoItemAdapter.UpsertOne(new { action.Id, action.Content }, state.Items)
                        });
                    }
                )
                .On<RemoveTodoItemAction>(
                    (state, action) =>
                    {
                        return state.With(new
                        {
                            Items = TodoItemAdapter.RemoveOne(action.Id, state.Items)
                        });
                    }
                )
                .ToList();
        }
    }
}
