using Microsoft.Toolkit.Uwp.UI.Controls;
using ReduxSimple.Samples.Common;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using static ReduxSimple.Samples.Extensions.FileExtensions;

namespace ReduxSimple.Samples.Components
{
    public sealed partial class DocumentationComponent : UserControl
    {
        public DocumentationComponent()
        {
            InitializeComponent();
        }

        public async Task LoadMarkdownFilesAsync(string folder)
        {
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
