using Converto;
using System.Collections.Generic;
using System.Linq;
using static ReduxSimple.Reducers;

namespace ReduxSimple.Uwp.Samples.TodoList
{
    public static class Reducers
    {
        public static IEnumerable<On<TodoListState>> CreateReducers()
        {
            return new List<On<TodoListState>>
            {
                On<SetFilterAction, TodoListState>(
                    (state, action) => state.With(new { action.Filter })
                ),
                On<CreateTodoItemAction, TodoListState>(
                    state =>
                    {
                        int newId = state.Items.Any() ? state.Items.Max(i => i.Id) + 1 : 1;
                        return state.With(new
                        {
                            Items = state.Items.Add(new TodoItem { Id = newId })
                        });
                    }
                ),
                On<CompleteTodoItemAction, TodoListState>(
                    (state, action) =>
                    {
                        var itemToUpdate = state.Items.Single(i => i.Id == action.Id && !i.Completed);
                        return state.With(new
                        {
                            Items = state.Items.Replace(itemToUpdate, itemToUpdate.With(new { Completed = true }))
                        });
                    }
                ),
                On<RevertCompleteTodoItemAction, TodoListState>(
                    (state, action) =>
                    {
                        var itemToUpdate = state.Items.Single(i => i.Id == action.Id && i.Completed);
                        return state.With(new
                        {
                            Items = state.Items.Replace(itemToUpdate, itemToUpdate.With(new { Completed = false }))
                        });
                    }
                ),
                On<UpdateTodoItemAction, TodoListState>(
                    (state, action) =>
                    {
                        var itemToUpdate = state.Items.Single(i => i.Id == action.Id);
                        return state.With(new
                        {
                            Items = state.Items.Replace(itemToUpdate, itemToUpdate.With(new { action.Content }))
                        });
                    }
                ),
                On<RemoveTodoItemAction, TodoListState>(
                    (state, action) => state.With(new { Items = state.Items.RemoveAll(i => i.Id == action.Id) })
                )
            };
        }
    }
}
