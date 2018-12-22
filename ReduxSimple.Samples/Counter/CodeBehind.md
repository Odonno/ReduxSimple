```csharp
public sealed partial class CounterPage : Page
{
    private static CounterStore _store = new CounterStore();

    public CounterPage()
    {
        InitializeComponent();

        // Observe changes on state
        _store.ObserveState()
            .Subscribe(state =>
            {
                CounterValueTextBlock.Text = state.Count.ToString();
            });

        // Observe UI events
        IncrementButton.Events().Click
            .Subscribe(_ => _store.Dispatch(new IncrementAction()));

        DecrementButton.Events().Click
            .Subscribe(_ => _store.Dispatch(new DecrementAction()));
    }
}
```