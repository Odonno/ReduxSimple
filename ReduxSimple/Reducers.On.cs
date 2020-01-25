using System;

namespace ReduxSimple
{
    public static partial class Reducers
    {
        /// <summary>
        /// Function used to filter reducer based on action type.
        /// </summary>
        /// <typeparam name="TAction">Action type.</typeparam>
        /// <typeparam name="TState">State type.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>A new On object that contains reducer with filtering options.</returns>
        public static On<TState> On<TAction, TState>(
            Func<TState, TAction, TState> reducer
        )
            where TState : class
            where TAction : class
        {
            return new On<TState>
            {
                Reduce = (state, action) =>
                {
                    if (!(action is TAction actionT1))
                        return state;

                    return reducer(state, actionT1);
                },
                Types = new[] { typeof(TAction).FullName }
            };
        }
        /// <summary>
        /// Function used to filter reducer based on action type.
        /// </summary>
        /// <typeparam name="TAction">Action type.</typeparam>
        /// <typeparam name="TState">State type.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>A new On object that contains reducer with filtering options.</returns>
        public static On<TState> On<TAction, TState>(
            Func<TState, TState> reducer
        )
            where TState : class
            where TAction : class
        {
            return new On<TState>
            {
                Reduce = (state, action) => reducer(state),
                Types = new[]
                {
                    typeof(TAction).FullName
                }
            };
        }

        /// <summary>
        /// Function used to filter reducer based on action type.
        /// </summary>
        /// <typeparam name="TAction1">Action type.</typeparam>
        /// <typeparam name="TAction2">Action type.</typeparam>
        /// <typeparam name="TState">State type.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>A new On object that contains reducer with filtering options.</returns>
        public static On<TState> On<TAction1, TAction2, TState>(
            Func<TState, TState> reducer
        )
            where TState : class
            where TAction1 : class
            where TAction2 : class
        {
            return new On<TState>
            {
                Reduce = (state, action) => reducer(state),
                Types = new[]
                {
                    typeof(TAction1).FullName,
                    typeof(TAction2).FullName
                }
            };
        }

        /// <summary>
        /// Function used to filter reducer based on action type.
        /// </summary>
        /// <typeparam name="TAction1">Action type.</typeparam>
        /// <typeparam name="TAction2">Action type.</typeparam>
        /// <typeparam name="TAction3">Action type.</typeparam>
        /// <typeparam name="TState">State type.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>A new On object that contains reducer with filtering options.</returns>
        public static On<TState> On<TAction1, TAction2, TAction3, TState>(
            Func<TState, TState> reducer
        )
            where TState : class
            where TAction1 : class
            where TAction2 : class
            where TAction3 : class
        {
            return new On<TState>
            {
                Reduce = (state, action) => reducer(state),
                Types = new[]
                {
                    typeof(TAction1).FullName,
                    typeof(TAction2).FullName,
                    typeof(TAction3).FullName
                }
            };
        }

        /// <summary>
        /// Function used to filter reducer based on action type.
        /// </summary>
        /// <typeparam name="TAction1">Action type.</typeparam>
        /// <typeparam name="TAction2">Action type.</typeparam>
        /// <typeparam name="TAction3">Action type.</typeparam>
        /// <typeparam name="TAction4">Action type.</typeparam>
        /// <typeparam name="TState">State type.</typeparam>
        /// <param name="reducer">Reducer function.</param>
        /// <returns>A new On object that contains reducer with filtering options.</returns>
        public static On<TState> On<TAction1, TAction2, TAction3, TAction4, TState>(
            Func<TState, TState> reducer
        )
            where TState : class
            where TAction1 : class
            where TAction2 : class
            where TAction3 : class
            where TAction4 : class
        {
            return new On<TState>
            {
                Reduce = (state, action) => reducer(state),
                Types = new[]
                {
                    typeof(TAction1).FullName,
                    typeof(TAction2).FullName,
                    typeof(TAction3).FullName,
                    typeof(TAction4).FullName
                }
            };
        }
    }
}
