using System.Reactive.Linq;
using static ReduxSimple.Effects;
using static ReduxSimple.Samples.TodoList.TodoListPage;
using static ReduxSimple.Samples.Common.EventTracking;

namespace ReduxSimple.Samples.TodoList
{
    public static class Effects
    {
        public static Effect<TodoListState> TrackAction = CreateEffect<TodoListState>(
            () => Store.ObserveAction()
                .Do(action =>
                {
                    TrackReduxAction(action);
                }),
            false
        );
    }
}
