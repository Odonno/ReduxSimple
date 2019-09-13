```csharp
public static class Reducers
{
    public static IEnumerable<On<CounterState>> CreateReducers()
    {
        return new List<On<CounterState>>
        {
            On<IncrementAction, CounterState>(
                state => state.With(new { Count = state.Count + 1 })
            ),
            On<DecrementAction, CounterState>(
                state => state.With(new { Count = state.Count - 1 })
            )
        };
    }
}
```