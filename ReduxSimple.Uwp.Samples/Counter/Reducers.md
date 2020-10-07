```csharp
public static class Reducers
{
    public static IEnumerable<On<RootState>> GetReducers()
    {
        return CreateSubReducers(SelectCounterState)
            .On<IncrementAction>(state => state.With(new { Count = state.Count + 1 }))
            .On<DecrementAction>(state => state.With(new { Count = state.Count - 1 }))
            .ToList();
    }
}
```