using Microsoft.Toolkit.Uwp.UI.Animations;
using ReduxSimple.Uwp.Samples.Components;
using System;
using System.Reactive.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using static ReduxSimple.Uwp.Samples.App;
using static ReduxSimple.Uwp.Samples.Counter.Selectors;

namespace ReduxSimple.Uwp.Samples.Counter
{
    public sealed partial class CounterPage : Page
    {
        public CounterPage()
        {
            InitializeComponent();

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

            // Initialize Components
            HistoryComponent.Initialize(Store);

            // Initialize Documentation
            DocumentationComponent.LoadMarkdownFilesAsync("Counter");

            GoToGitHubButton.Events().Click
                .Subscribe(async _ =>
                {
                    var uri = new Uri("https://github.com/Odonno/ReduxSimple/tree/master/ReduxSimple.Samples/Counter");
                    await Launcher.LaunchUriAsync(uri);
                });

            ContentGrid.Events().Tapped
                .Subscribe(_ => DocumentationComponent.Collapse());
            DocumentationComponent.ObserveOnExpanded()
                .Subscribe(_ => ContentGrid.Blur(5).Start());
            DocumentationComponent.ObserveOnCollapsed()
                .Subscribe(_ => ContentGrid.Blur(0).Start());
        }
    }
}
