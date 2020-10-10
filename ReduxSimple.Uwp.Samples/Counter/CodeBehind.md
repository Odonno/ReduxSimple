```csharp
public sealed partial class CounterPage : Page
{
    public CounterPage()
    {
        InitializeComponent();

        // Observe changes on state
        Store.Select(SelectCount)
            .UntilDestroyed(this)
            .Subscribe(state =>
            {
                CounterValueTextBlock.Text = state.Count.ToString();
            });

        // Observe UI events
        IncrementButton.Events().Click
            .Subscribe(_ => Store.Dispatch(new IncrementAction()));

        DecrementButton.Events().Click
            .Subscribe(_ => Store.Dispatch(new DecrementAction()));
    }
}
```