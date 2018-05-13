using ReduxSimple.Samples.Components;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace ReduxSimple.Samples.Counter
{
    public sealed partial class CounterPage : Page
    {
        private static CounterStore _store = new CounterStore();

        public CounterPage()
        {
            InitializeComponent();

            // Reset Store (due to HistoryComponent lifecycle)
            _store.Reset();

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

            // Initialize UI
            CounterValueTextBlock.Text = _store.State.Count.ToString();

            // Initialize Components
            HistoryComponent.Initialize(_store);

            // Initialize Documentation
            DocumentationComponent.LoadMarkdownFilesAsync("Counter");
        }
    }
}
