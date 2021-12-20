using Converto;

namespace ReduxSimple;

internal class ExplicitStateLens<TState, TFeatureState> : BaseStateLens<TState, TFeatureState>
    where TState : class, new()
    where TFeatureState : class, new()
{
    private readonly Func<TState, TFeatureState> _featureSelector;
    private readonly Func<TState, TFeatureState, TState> _stateReducer;

    public ExplicitStateLens(
        Func<TState, TFeatureState> featureSelector,
        Func<TState, TFeatureState, TState> stateReducer
    )
    {
        _featureSelector = featureSelector;
        _stateReducer = stateReducer;
    }

    public override On<TState> CreateParentReducer(On<TFeatureState> on)
    {
        return new On<TState>
        {
            Reduce = (state, action) =>
            {
                if (on?.Reduce == null)
                    return state;

                var featureState = _featureSelector(state);
                var reducerResult = on.Reduce(featureState, action);

                if (featureState.IsDeepEqual(reducerResult))
                    return state;

                return _stateReducer(state, reducerResult);
            },
            Types = on.Types
        };
    }
}
internal class ExplicitStateLens<TState, TAction, TFeatureState> : BaseStateLens<TState, TFeatureState>
    where TState : class, new()
    where TFeatureState : class, new()
{
    private readonly Func<TState, TAction, TFeatureState> _featureSelector;
    private readonly Func<TState, TAction, TFeatureState, TState> _stateReducer;

    public ExplicitStateLens(
        Func<TState, TAction, TFeatureState> featureSelector,
        Func<TState, TAction, TFeatureState, TState> stateReducer
    )
    {
        _featureSelector = featureSelector;
        _stateReducer = stateReducer;
    }

    public override On<TState> CreateParentReducer(On<TFeatureState> on)
    {
        return new On<TState>
        {
            Reduce = (state, action) =>
            {
                if (on?.Reduce == null)
                    return state;

                if (action is TAction typedAction)
                {
                    var featureState = _featureSelector(state, typedAction);
                    var reducerResult = on.Reduce(featureState, action);

                    if (featureState.IsDeepEqual(reducerResult))
                        return state;

                    return _stateReducer(state, typedAction, reducerResult);
                }

                throw new NotSupportedException(
                    $"This type `{typeof(TAction).Name}` of action cannot be used in the reducer reducer of `{typeof(TFeatureState).Name}` inside `{typeof(TState).Name}`."
                );
            },
            Types = on.Types
        };
    }
}
