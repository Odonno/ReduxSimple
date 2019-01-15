```csharp
public class CounterStore : ReduxStore<CounterState>
{
    protected override CounterState Reduce(CounterState state, object action)
    {
        if (action is IncrementAction _)
        {
            return state.With(new { Count = state.Count + 1 });
        }
        if (action is DecrementAction _)
        {
            return state.With(new { Count = state.Count - 1 });
        }
        return base.Reduce(state, action);
    }
}
```