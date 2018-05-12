using Microsoft.Toolkit.Uwp.UI.Controls;
using ReduxSimple.Samples.Common;
using ReduxSimple.Samples.Components;
using ReduxSimple.Samples.Extensions;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using static ReduxSimple.Samples.Extensions.FileExtensions;

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
            IncrementButton.ObserveOnClick()
                .Subscribe(_ => _store.Dispatch(new IncrementAction()));

            DecrementButton.ObserveOnClick()
                .Subscribe(_ => _store.Dispatch(new DecrementAction()));

            // Initialize UI
            CounterValueTextBlock.Text = _store.State.Count.ToString();

            // Initialize Components
            HistoryComponent.Initialize(_store);

            // Initialize Documentation
            InitializeDocumentationAsync();
        }

        private async void InitializeDocumentationAsync()
        {
            const string folder = "Counter";

            await LoadMarkdownComponent(IntroductionMarkdownTextBlock, $"{folder}/Introduction.md");
            await LoadMarkdownComponent(StateMarkdownTextBlock, $"{folder}/State.md");
            await LoadMarkdownComponent(ActionsMarkdownTextBlock, $"{folder}/Actions.md");
            await LoadMarkdownComponent(StoreMarkdownTextBlock, $"{folder}/Store.md");
            await LoadMarkdownComponent(UserInterfaceMarkdownTextBlock, $"{folder}/UI.md");
            await LoadMarkdownComponent(CodeBehindMarkdownTextBlock, $"{folder}/CodeBehind.md");
            await LoadMarkdownComponent(DependenciesMarkdownTextBlock, $"{folder}/Dependencies.md");
        }

        private async Task LoadMarkdownComponent(MarkdownTextBlock markdownTextBlock, string filePath)
        {
            markdownTextBlock.SetRenderer<CodeMarkdownRenderer>();
            markdownTextBlock.Text = await ReadFileAsync(filePath);
        }
    }
}
