using ReduxSimple.Samples.Common;
using ReduxSimple.Samples.Components;
using ReduxSimple.Samples.Extensions;
using System;
using System.Reactive.Linq;
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
            HistoryComponent.Store = _store;

            // Initialize Documentation
            InitializeDocumentationAsync();
        }

        private async void InitializeDocumentationAsync()
        {
            const string folder = "Counter";

            IntroductionMarkdownTextBlock.SetRenderer<CodeMarkdownRenderer>();
            IntroductionMarkdownTextBlock.Text = await ReadFileAsync($"{folder}/Introduction.md");

            StateMarkdownTextBlock.SetRenderer<CodeMarkdownRenderer>();
            StateMarkdownTextBlock.Text = await ReadFileAsync($"{folder}/State.md");

            ActionsMarkdownTextBlock.SetRenderer<CodeMarkdownRenderer>();
            ActionsMarkdownTextBlock.Text = await ReadFileAsync($"{folder}/Actions.md");

            StoreMarkdownTextBlock.SetRenderer<CodeMarkdownRenderer>();
            StoreMarkdownTextBlock.Text = await ReadFileAsync($"{folder}/Store.md");

            UserInterfaceMarkdownTextBlock.SetRenderer<CodeMarkdownRenderer>();
            UserInterfaceMarkdownTextBlock.Text = await ReadFileAsync($"{folder}/UI.md");

            CodeBehindMarkdownTextBlock.SetRenderer<CodeMarkdownRenderer>();
            CodeBehindMarkdownTextBlock.Text = await ReadFileAsync($"{folder}/CodeBehind.md");

            DependenciesMarkdownTextBlock.SetRenderer<CodeMarkdownRenderer>();
            DependenciesMarkdownTextBlock.Text = await ReadFileAsync($"{folder}/Dependencies.md");
        }
    }
}
