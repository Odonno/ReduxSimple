using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReduxSimple
{
    public abstract class ReduxStoreWithHistory<TState> : ReduxStore<TState>
        where TState : class, new()
    {
        private readonly Stack<TState> _previousStates = new Stack<TState>();
        private readonly Stack<TState> _nextStates = new Stack<TState>();

        public bool CanUndo => _previousStates.Count != 0;

        public bool CanRedo => _nextStates.Count != 0;

        protected ReduxStoreWithHistory(TState initialState = null) : base(initialState)
        { }

        public override void Dispatch(object action)
        {
            _nextStates.Clear();
            _previousStates.Push(State);

            base.Dispatch(action);
        }

        public bool Undo()
        {
            if (!CanUndo)
            {
                return false;
            }

            _nextStates.Push(State);

            GoToState(_previousStates.Pop());

            return true;
        }

        public bool Redo()
        {
            if (!CanRedo)
            {
                return false;
            }

            _previousStates.Push(State);

            GoToState(_nextStates.Pop());

            return true;
        }
    }
}
