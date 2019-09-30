using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReduxSimple
{
    /// <summary>
    /// The <see cref="ReduxStore{TState}" /> is a centralized object for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public sealed partial class ReduxStore<TState> where TState : class, new()
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

        /// <summary>
        /// Gets a value indicating whether time-travel is enabled in the store.
        /// </summary>
        public bool TimeTravelEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether the undo operation can be performed.
        /// </summary>
        public bool CanUndo => _pastMementos.Count > 1;
        /// <summary>
        /// Gets a value indicating whether the redo operation can be performed.
        /// </summary>
        public bool CanRedo => _futureActions.Count != 0;

        /// <summary>
        /// Undoes the last action that was performed, if any.
        /// </summary>
        /// <returns><c>true</c> if an action was undone; <c>false</c> otherwise.</returns>
        public bool Undo()
        {
            if (!TimeTravelEnabled)
            {
                return false;
            }

            if (!CanUndo)
            {
                return false;
            }

            var memento = _pastMementos.Pop();
            _futureActions.Push(memento.Action);
            UpdateState(memento.State);
            _undoneActionSubject.OnNext(memento.Action);

            return true;
        }

        /// <summary>
        /// Observes changes on CanUndo property.
        /// </summary>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about CanUndo property.</returns>
        public IObservable<bool> ObserveCanUndo()
        {
            return Observable.Merge(ObserveAction(ActionOriginFilter.All), ObserveReset(), ObserveUndoneAction())
                .Select(_ => CanUndo)
                .StartWith(CanUndo)
                .DistinctUntilChanged();
        }

        /// <summary>
        /// Redoes an operation, if any, that was previously undone.
        /// </summary>
        /// <returns><c>true</c> if an action was redone; <c>false</c> otherwise.</returns>
        public bool Redo()
        {
            if (!TimeTravelEnabled)
            {
                return false;
            }

            if (!CanRedo)
            {
                return false;
            }

            Dispatch(_futureActions.Pop(), false);

            return true;
        }

        /// <summary>
        /// Observes changes on CanRedo property.
        /// </summary>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about CanRedo property.</returns>
        public IObservable<bool> ObserveCanRedo()
        {
            return Observable.Merge(ObserveAction(ActionOriginFilter.All), ObserveReset(), ObserveUndoneAction())
                .Select(_ => CanRedo)
                .StartWith(CanRedo)
                .DistinctUntilChanged();
        }

        /// <summary>
        /// Observes actions that are reversed (undone).
        /// </summary>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about actions that are reversed (undone).</returns>
        public IObservable<object> ObserveUndoneAction()
        {
            return _undoneActionSubject;
        }
        /// <summary>
        /// Observes actions of a specific type that are reversed (undone).
        /// </summary>
        /// <typeparam name="T">The type of actions that the subscriber is interested in.</typeparam>
        /// <returns>
        /// An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates whenever an action of <typeparamref name="T"/> is reversed (undone).
        /// </returns>
        public IObservable<T> ObserveUndoneAction<T>()
        {
            return _undoneActionSubject.OfType<T>();
        }
    }
}