namespace ReduxSimple;

internal abstract class BaseStateLens<TState, TFeatureState> : IStateLens<TState, TFeatureState>
    where TState : class, new()
    where TFeatureState : class, new()
{
    private readonly List<On<TState>> _ons = new List<On<TState>>();

    public abstract On<TState> CreateParentReducer(On<TFeatureState> on);

    public IStateLens<TState, TFeatureState> On<TAction>(Func<TFeatureState, TAction, TFeatureState> featureReducer) where TAction : class
    {
        _ons.Add(
            CreateParentReducer(Reducers.On(featureReducer))
        );
        return this;
    }

    public IStateLens<TState, TFeatureState> On<TAction>(Func<TFeatureState, TFeatureState> featureReducer) where TAction : class
    {
        _ons.Add(
            CreateParentReducer(Reducers.On<TAction, TFeatureState>(featureReducer))
        );
        return this;
    }

    public IStateLens<TState, TFeatureState> On<TAction1, TAction2>(Func<TFeatureState, TFeatureState> featureReducer)
        where TAction1 : class
        where TAction2 : class
    {
        _ons.Add(
            CreateParentReducer(Reducers.On<TAction1, TAction2, TFeatureState>(featureReducer))
        );
        return this;
    }

    public IStateLens<TState, TFeatureState> On<TAction1, TAction2, TAction3>(Func<TFeatureState, TFeatureState> featureReducer)
        where TAction1 : class
        where TAction2 : class
        where TAction3 : class
    {
        _ons.Add(
            CreateParentReducer(Reducers.On<TAction1, TAction2, TAction3, TFeatureState>(featureReducer))
        );
        return this;
    }

    public IStateLens<TState, TFeatureState> On<TAction1, TAction2, TAction3, TAction4>(Func<TFeatureState, TFeatureState> featureReducer)
        where TAction1 : class
        where TAction2 : class
        where TAction3 : class
        where TAction4 : class
    {
        _ons.Add(
            CreateParentReducer(Reducers.On<TAction1, TAction2, TAction3, TAction4, TFeatureState>(featureReducer))
        );
        return this;
    }

    public IEnumerable<On<TState>> ToList()
    {
        return _ons;
    }
}
