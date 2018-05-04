using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ReduxSimple.Samples.Counter
{
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
            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => IncrementButton.Click += h,
                h => IncrementButton.Click -= h
            )
                .Subscribe(e =>
                {
                    _store.Dispatch(new IncrementAction());
                });

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => DecrementButton.Click += h,
                h => DecrementButton.Click -= h
            )
                .Subscribe(e =>
                {
                    _store.Dispatch(new DecrementAction());
                });

            // Initialize UI
            CounterValueTextBlock.Text = _store.State.Count.ToString();
        }
    }
}
