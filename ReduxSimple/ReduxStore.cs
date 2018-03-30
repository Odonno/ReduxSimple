﻿using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace ReduxSimple
{
    public abstract class ReduxStore<TState> where TState : class, new()
    {
        private readonly TState _initialState;
        private readonly Subject<TState> _stateSubject = new Subject<TState>();
        private readonly Subject<object> _actionSubject = new Subject<object>();

        public TState State { get; private set; }

        protected ReduxStore(TState initialState = null)
        {
            State = _initialState = initialState ?? new TState();
        }

        public virtual void Dispatch(object action)
        {
            UpdateState(Reduce(State, action));
            _actionSubject.OnNext(action);
        }

        public virtual TState Reduce(TState state, object action)
        {
            return state;
        }

        public IObservable<TState> ObserveState()
        {
            return _stateSubject.AsObservable().DistinctUntilChanged();
        }
        public IObservable<TPartial> ObserveState<TPartial>(Func<TState, TPartial> selector)
        {
            return _stateSubject.Select(selector).DistinctUntilChanged();
        }

        public IObservable<object> ObserveAction()
        {
            return _actionSubject.AsObservable();
        }
        public IObservable<T> ObserveAction<T>() where T : class
        {
            return _actionSubject.OfType<T>().AsObservable();
        }

        public virtual void Reset()
        {
            UpdateState(_initialState);
        }

        protected void UpdateState(TState state)
        {
            State = state;
            _stateSubject.OnNext(State);
        }
    }
}