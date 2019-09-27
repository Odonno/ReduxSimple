using System;
using System.Linq;
using System.Reactive.Linq;

namespace ReduxSimple
{
    /// <summary>
    /// The <see cref="ReduxStore{TState}" /> is a centralized object for creating predictable state containers.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    public sealed partial class ReduxStore<TState> where TState : class, new()
    {
        private readonly FullStateComparer<TState> _fullStateComparer = new FullStateComparer<TState>();

        /// <summary>
        /// Select the full state object.
        /// </summary>
        /// <returns>An <see cref="IObservable{T}"/> that can be subscribed to in order to receive updates about state changes.</returns>
        public IObservable<TState> Select()
        {
            return _stateSubject
                .DistinctUntilChanged(_fullStateComparer);
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TResult">The type of the selector result.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TResult>(Func<TState, TResult> selector)
        {
            return _stateSubject
                .Select(selector)
                .DistinctUntilChanged();
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TResult">The type of the selector result.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TResult>(ISelectorWithoutProps<TState, TResult> selector)
        {
            return selector.Apply(_stateSubject);
        }
        /// <summary>
        /// Select a value derived from the state of the store.
        /// </summary>
        /// <typeparam name="TProps">The type of the props to use in the selector.</typeparam>
        /// <typeparam name="TResult">The type of the selector result.</typeparam>
        /// <param name="selector">
        /// The mapping function that can be applied to get the desired partial state of type <typeparamref name="TResult"/> from an instance of <typeparamref name="TState"/>.
        /// </param>
        /// <param name="props">
        /// The properties used in the selector.
        /// </param>
        /// <returns></returns>
        public IObservable<TResult> Select<TProps, TResult>(ISelectorWithProps<TState, TProps, TResult> selector, TProps props)
        {
            return selector.Apply(_stateSubject, props);
        }
    }
}