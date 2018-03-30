using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ReduxSimple
{
    public abstract class ReduxStoreWithHistory<TState> : ReduxStore<TState>
        where TState : class, new()
    {
        private class ReduxStoreMemento
        {
            public TState State { get; }
            public object Action { get; }

            public ReduxStoreMemento(TState state, object action)
            {
                State = state;
                Action = action;
            }
        }

        private readonly Subject<object> _undoneActionSubject = new Subject<object>();
        private readonly Stack<ReduxStoreMemento> _pastMementos = new Stack<ReduxStoreMemento>();
        private readonly Stack<object> _futureActions = new Stack<object>();
        
        public bool CanUndo => _pastMementos.Count != 0;
        public bool CanRedo => _futureActions.Count != 0;

        protected ReduxStoreWithHistory(TState initialState = null) : base(initialState)
        { }

        public override void Dispatch(object action)
        {
            Dispatch(action, true);
        }
        private void Dispatch(object action, bool clearFuture)
        {
            if (clearFuture)
            {
                _futureActions.Clear();
            }

            _pastMementos.Push(new ReduxStoreMemento(State, action));

            base.Dispatch(action);
        }

        public bool Undo()
        {
            if (!CanUndo)
            {
                return false;
            }

            var memento = _pastMementos.Pop();
            UpdateState(memento.State);
            _undoneActionSubject.OnNext(memento.Action);

            return true;
        }

        public bool Redo()
        {
            if (!CanRedo)
            {
                return false;
            }

            Dispatch(_futureActions.Pop(), false);

            return true;
        }

        public IObservable<object> ObserveUndoneAction()
        {
            return _undoneActionSubject.AsObservable();
        }
        public IObservable<T> ObserveUndoneAction<T>() where T : class
        {
            return _undoneActionSubject.OfType<T>().AsObservable();
        }

        public override void Reset()
        {
            _pastMementos.Clear();
            _futureActions.Clear();
            base.Reset();
        }
    }
}
