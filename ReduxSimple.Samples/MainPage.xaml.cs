using ReduxSimple.Samples.Counter;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ReduxSimple.Samples
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => GoToCounterButton.Click += h,
                h => GoToCounterButton.Click -= h
            )
                .Subscribe(_ =>
                {
                    Frame.Navigate(typeof(CounterPage));
                });
        }
    }
}
