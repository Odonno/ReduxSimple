```csharp
public static class Selectors
{
    public static Func<CounterState, int> SelectCount = state => state.Count;
}
```