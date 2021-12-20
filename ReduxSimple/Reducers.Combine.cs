namespace ReduxSimple;

public static partial class Reducers
{
    public static IEnumerable<On<TState>> CombineReducers<TState>(params IEnumerable<On<TState>>[] reducersList)
        where TState : class
    {
        var result = new List<On<TState>>();

        foreach (var reducers in reducersList)
        {
            result.AddRange(reducers);
        }

        return result;
    }
}
