using Microsoft.Toolkit.Uwp.UI.Animations;
using ReduxSimple.Samples.Components;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using static ReduxSimple.Samples.Counter.Reducers;
using static ReduxSimple.Samples.Counter.Selectors;
using static ReduxSimple.Samples.Counter.Effects;

namespace ReduxSimple.Samples.Counter
{
    public sealed partial class CounterPage : Page
    {
        public static readonly ReduxStore<CounterState> Store = 
            new ReduxStore<CounterState>(CreateReducers(), true);

        public CounterPage()
        {
            InitializeComponent();

            // Reset Store (due to HistoryComponent lifecycle)
            Store.Reset();

            // Observe changes on state
            Store.Select(SelectCount)
                .Subscribe(count =>
                {
                    CounterValueTextBlock.Text = count.ToString();
                });

            // Observe UI events
            IncrementButton.Events().Click
                .Subscribe(_ => Store.Dispatch(new IncrementAction()));

            DecrementButton.Events().Click
                .Subscribe(_ => Store.Dispatch(new DecrementAction()));

            // Register Effects
            Store.RegisterEffects(
                TrackAction
            );

            // Initialize Components
            HistoryComponent.Initialize(Store);

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
