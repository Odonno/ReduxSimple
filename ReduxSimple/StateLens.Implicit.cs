using Converto;
using System;
using System.Linq;

namespace ReduxSimple
{
    internal class ImplicitStateLens<TState, TFeatureState> : BaseStateLens<TState, TFeatureState>
        where TState : class, new()
        where TFeatureState : class, new()
    {
        private readonly Func<TState, TFeatureState> _featureSelector;

        public ImplicitStateLens(
            Func<TState, TFeatureState> featureSelector
        )
        {
            _featureSelector = featureSelector;
        }

        public override On<TState> CreateParentReducer(On<TFeatureState> on)
        {
            var parentStateProperties = typeof(TState).GetProperties();

            return new On<TState>
            {
                Reduce = (state, action) =>
                {
                    if (on?.Reduce == null)
                        return state;

                    var featureState = _featureSelector(state);
                    var reducerResult = on.Reduce(featureState, action);

                    if (featureState.IsDeepEqual(reducerResult))
                    {
                        return state;
                    }

                    var featureProperty = parentStateProperties
                        .SingleOrDefault(p =>
                        {
                            return p.GetValue(state) == featureState;
                        });

                    if (featureProperty == null)
                    {
                        throw new NotSupportedException(
                            $"A sub-reducer cannot find the feature reducer of `{typeof(TFeatureState).Name}` inside `{typeof(TState).Name}`."
                        );
                    }

                    var stateCopy = state.Copy();
                    featureProperty.SetValue(stateCopy, reducerResult);

                    return stateCopy;
                },
                Types = on.Types
            };
        }
    }
}
