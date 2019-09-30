using System.Collections.Generic;
using System.Reactive.Subjects;

namespace ReduxSimple
{
    /// <summary>
    /// The <see cref="ReduxStore{TState}" /> is a centralized object for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public sealed partial class ReduxStore<TState> where TState : class, new()
    {
        private readonly TState _initialState;
        private readonly BehaviorSubject<TState> _stateSubject;
               
        /// <summary>
        /// Gets the current state of the store.
        /// </summary>
        public TState State { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReduxStore{TState}"/> class.
        /// </summary>
        /// <param name="reducers">A list of reducers to update state when an action is triggered.</param>
        /// <param name="enableTimeTravel">Enable time-travel operations (undo, redo).</param>
        public ReduxStore(
            IEnumerable<On<TState>> reducers,
            bool enableTimeTravel = false
        ) : this(reducers, null, enableTimeTravel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReduxStore{TState}"/> class.
        /// </summary>
        /// <param name="reducers">A list of reducers to update state when an action is triggered.</param>
        /// <param name="initialState">The initial state to put the store in; if <c>null</c>, a default value is constructed using <c>new TState()</c>.</param>
        /// <param name="enableTimeTravel">Enable time-travel operations (undo, redo).</param>
        public ReduxStore(
            IEnumerable<On<TState>> reducers,
            TState initialState,
            bool enableTimeTravel = false
        )
        {
            _reducers = new List<On<TState>>(reducers);
            State = _initialState = initialState ?? new TState();
            _stateSubject = new BehaviorSubject<TState>(State);
            TimeTravelEnabled = enableTimeTravel;

            Dispatch(new InitializeStoreAction());
        }
    }
}