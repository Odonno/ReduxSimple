using Converto;
using System;
using System.Linq;
using static Converto.Main;

namespace ReduxSimple
{
    public static partial class Reducers
    {
        public static On<TState>[] CreateSubReducers<TState, TFeatureState>(
            On<TFeatureState>[] routerReducers,
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

            return routerReducers
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
    }
}
