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
        IncrementButton.Events().Tapped
            .Subscribe(_ => _store.Dispatch(new IncrementAction()));

        DecrementButton.Events().Tapped
            .Subscribe(_ => _store.Dispatch(new DecrementAction()));

        // Initialize UI
        CounterValueTextBlock.Text = _store.State.Count.ToString();
    }
}
```