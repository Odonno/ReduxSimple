using Windows.UI.Xaml;

namespace ReduxSimple.Uwp.Samples.Extensions
{
    public static class UIElementExtensions
    {
        public static void ShowIf(this UIElement element, bool value)
        {
            element.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
        public static void HideIf(this UIElement element, bool value)
        {
            element.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
