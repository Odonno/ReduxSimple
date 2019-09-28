using Converto;
using System;
using System.Linq;
using static Converto.Main;

namespace ReduxSimple
{
    public static partial class Reducers
    {
        /// <summary>
        /// Create a sub-reducers, for features-like purpose.
        /// </summary>
        /// <typeparam name="TState">Type of the root state.</typeparam>
        /// <typeparam name="TFeatureState">Type of the feature state.</typeparam>
        /// <param name="featureReducers">Reducers that directly use the feature state.</param>
        /// <param name="selectFeature">Select the feature from the root state.</param>
        /// <returns>Returns reducers targeting the root state.</returns>
        public static On<TState>[] CreateSubReducers<TState, TFeatureState>(
            On<TFeatureState>[] featureReducers,
            Func<TState, TFeatureState> selectFeature
        )
            where TState : class, new()
            where TFeatureState : class, new()
        {
            var selectFeatureType = selectFeature.GetType();

            var selectFeatureInputType = selectFeatureType.GenericTypeArguments[0];
            var selectFeatureOutputType = selectFeatureType.GenericTypeArguments[1];

            var featureProperty = selectFeatureInputType.GetProperties()
                .Single(p => p.PropertyType.FullName == selectFeatureOutputType.FullName);

            return featureReducers
                .Select(r =>
                {
                    return new On<TState>
                    {
                        Reduce = (state, action) =>
                        {
                            var featureState = selectFeature(state);
                            var reducerResult = r.Reduce(featureState, action);

                            if (IsDeepEqual(featureState, reducerResult))
                            {
                                return state;
                            }

                            var stateCopy = state.Copy();
                            featureProperty.SetValue(stateCopy, reducerResult);

                            return stateCopy;
                        },
                        Types = r.Types
                    };
                })
                .ToArray();
        }
        /// <summary>
        /// Create a sub-reducers, for features-like purpose.
        /// </summary>
        /// <typeparam name="TState">Type of the root state.</typeparam>
        /// <typeparam name="TFeatureState">Type of the feature state.</typeparam>
        /// <param name="featureReducers">Reducers that directly use the feature state.</param>
        /// <param name="selectFeature">Select the feature from the root state.</param>
        /// <returns>Returns reducers targeting the root state.</returns>
        public static On<TState>[] CreateSubReducers<TState, TFeatureState>(
            On<TFeatureState>[] featureReducers,
            ISelectorWithoutProps<TState, TFeatureState> selectFeature
        )
            where TState : class, new()
            where TFeatureState : class, new()
        {
            var selectFeatureType = selectFeature.GetType();

            var selectFeatureInputType = selectFeatureType.GenericTypeArguments[0];
            var selectFeatureOutputType = selectFeatureType.GenericTypeArguments[1];

            var featureProperty = selectFeatureInputType.GetProperties()
                .Single(p => p.PropertyType.FullName == selectFeatureOutputType.FullName);

            return featureReducers
                .Select(r =>
                {
                    return new On<TState>
                    {
                        Reduce = (state, action) =>
                        {
                            var featureState = selectFeature.Apply(state);
                            var reducerResult = r.Reduce(featureState, action);

                            if (IsDeepEqual(featureState, reducerResult))
                            {
                                return state;
                            }

                            var stateCopy = state.Copy();
                            featureProperty.SetValue(stateCopy, reducerResult);

                            return stateCopy;
                        },
                        Types = r.Types
                    };
                })
                .ToArray();
        }
    }
}
