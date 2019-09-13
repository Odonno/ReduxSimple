using Microsoft.Toolkit.Uwp.UI.Animations;
using ReduxSimple.Samples.Components;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using static ReduxSimple.Samples.Counter.Reducers;
using static ReduxSimple.Samples.Counter.Selectors;

namespace ReduxSimple.Samples.Counter
{
    public sealed partial class CounterPage : Page
    {
        private static readonly ReduxStoreWithHistory<CounterState> _store = 
            new ReduxStoreWithHistory<CounterState>(CreateReducers());

        public CounterPage()
        {
            InitializeComponent();

            // Reset Store (due to HistoryComponent lifecycle)
            _store.Reset();

            // Observe changes on state
            _store.Select(SelectCount)
                .Subscribe(count =>
                {
                    CounterValueTextBlock.Text = count.ToString();
                });

            // Observe UI events
            IncrementButton.Events().Click
                .Subscribe(_ => _store.Dispatch(new IncrementAction()));

            DecrementButton.Events().Click
                .Subscribe(_ => _store.Dispatch(new DecrementAction()));

            // Initialize Components
            HistoryComponent.Initialize(_store);

            // Initialize Documentation
            DocumentationComponent.LoadMarkdownFilesAsync("Counter");

            ContentGrid.Events().Tapped
                .Subscribe(_ => DocumentationComponent.Collapse());
            DocumentationComponent.ObserveOnExpanded()
                .Subscribe(_ => ContentGrid.Blur(5).Start());
            DocumentationComponent.ObserveOnCollapsed()
                .Subscribe(_ => ContentGrid.Blur(0).Start());
        }
    }
}
