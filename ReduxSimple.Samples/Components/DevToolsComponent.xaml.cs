using Microsoft.Toolkit.Uwp.Helpers;
using Windows.UI.Xaml.Controls;

namespace ReduxSimple.Uwp.Samples.Components
{
    public sealed partial class DevToolsComponent : Page
    {
        public DevToolsComponent()
        {
            InitializeComponent();

            PageNameTextBlock.Text = "Redux DevTools - " + SystemInformation.ApplicationName;
        }

        internal void Initialize<TState>(ReduxStore<TState> store) where TState : class, new()
        {
        }
    }
}
