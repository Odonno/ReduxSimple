using Converto;
using System.Linq;
using static ReduxSimple.Samples.Common.EventTracking;

namespace ReduxSimple.Samples.TodoList
{
    public class TodoListStore : ReduxStoreWithHistory<TodoListState>
    {
        protected override TodoListState Reduce(TodoListState state, object action)
        {
            TrackReduxAction(action);

            if (action is SetFilterAction setFilterAction)
            {
                return state.With(new
                {
                    Filter = setFilterAction.Filter
                });
            }
            if (action is CreateTodoItemAction createTodoItemAction)
            {
                int newId = state.Items.Any() ? state.Items.Max(i => i.Id) + 1 : 1;

                return state.With(new
                {
                    Items = state.Items.Add(new TodoItem { Id = newId })
                });
            }
            if (action is CompleteTodoItemAction completeTodoItemAction)
            {
                var itemToUpdate = state.Items.Single(i => i.Id == completeTodoItemAction.Id && !i.Completed);

                return state.With(new
                {
                    Items = state.Items.Replace(itemToUpdate, itemToUpdate.With(new { Completed = true }))
                });
            }
            if (action is RevertCompleteTodoItemAction revertCompleteTodoItemAction)
            {
                var itemToUpdate = state.Items.Single(i => i.Id == revertCompleteTodoItemAction.Id && i.Completed);

                return state.With(new
                {
                    Items = state.Items.Replace(itemToUpdate, itemToUpdate.With(new { Completed = false }))
                });
            }
            if (action is UpdateTodoItemAction updateTodoItemAction)
            {
                var itemToUpdate = state.Items.Single(i => i.Id == updateTodoItemAction.Id);

                return state.With(new
                {
                    Items = state.Items.Replace(itemToUpdate, itemToUpdate.With(new { Content = updateTodoItemAction.Content }))
                });
            }
            if (action is RemoveTodoItemAction removeTodoItemAction)
            {
                return state.With(new
                {
                    Items = state.Items.RemoveAll(i => i.Id == removeTodoItemAction.Id)
                });
            }
            return base.Reduce(state, action);
        }
    }
}
