using System.Linq;

namespace ReduxSimple.Samples.TodoList
{
    public class TodoListStore : ReduxStoreWithHistory<TodoListState>
    {
        protected override TodoListState Reduce(TodoListState state, object action)
        {
            if (action is SetFilterAction setFilterAction)
            {
                return new TodoListState
                {
                    Items = state.Items,
                    Filter = setFilterAction.Filter
                };
            }
            if (action is CreateTodoItemAction createTodoItemAction)
            {
                int newId = state.Items.Any() ? state.Items.Max(i => i.Id) + 1 : 1;

                return new TodoListState
                {
                    Items = state.Items.Add(new TodoItem { Id = newId }),
                    Filter = state.Filter
                };
            }
            if (action is CompleteTodoItemAction completeTodoItemAction)
            {
                var itemToUpdate = state.Items.Single(i => i.Id == completeTodoItemAction.Id && !i.Completed);

                return new TodoListState
                {
                    Items = state.Items.Replace(itemToUpdate, new TodoItem { Id = itemToUpdate.Id, Content = itemToUpdate.Content, Completed = true }),
                    Filter = state.Filter
                };
            }
            if (action is RevertCompleteTodoItemAction revertCompleteTodoItemAction)
            {
                var itemToUpdate = state.Items.Single(i => i.Id == revertCompleteTodoItemAction.Id && i.Completed);

                return new TodoListState
                {
                    Items = state.Items.Replace(itemToUpdate, new TodoItem { Id = itemToUpdate.Id, Content = itemToUpdate.Content, Completed = false }),
                    Filter = state.Filter
                };
            }
            if (action is UpdateTodoItemAction updateTodoItemAction)
            {
                var itemToUpdate = state.Items.Single(i => i.Id == updateTodoItemAction.Id);

                return new TodoListState
                {
                    Items = state.Items.Replace(itemToUpdate, new TodoItem { Id = itemToUpdate.Id, Content = updateTodoItemAction.Content, Completed = itemToUpdate.Completed }),
                    Filter = state.Filter
                };
            }
            if (action is RemoveTodoItemAction removeTodoItemAction)
            {
                return new TodoListState
                {
                    Items = state.Items.RemoveAll(i => i.Id == removeTodoItemAction.Id),
                    Filter = state.Filter
                };
            }
            return base.Reduce(state, action);
        }
    }
}
