```csharp
public sealed partial class CounterPage : Page
{
    public static readonly ReduxStore<CounterState> Store = 
        new ReduxStore<CounterState>(CreateReducers(), true);

    public CounterPage()
    {
        InitializeComponent();

        // Observe changes on state
        _store.Select(SelectCount)
            .Subscribe(state =>
            {
                CounterValueTextBlock.Text = state.Count.ToString();
            });

        // Observe UI events
        IncrementButton.Events().Click
            .Subscribe(_ => _store.Dispatch(new IncrementAction()));

        DecrementButton.Events().Click
            .Subscribe(_ => _store.Dispatch(new DecrementAction()));

        // Register Effects
        Store.RegisterEffects(
            TrackAction
        );
    }
}
```