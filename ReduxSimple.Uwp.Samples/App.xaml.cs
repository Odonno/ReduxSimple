using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using SuccincT.Options;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using static ReduxSimple.Uwp.Samples.Reducers;
using static ReduxSimple.Uwp.Samples.Effects;
using ReduxSimple.Uwp.RouterStore;

namespace ReduxSimple.Uwp.Samples
{
    public sealed partial class App : Application
    {
        public static readonly ReduxStore<RootState> Store =
            new ReduxStore<RootState>(CreateReducers(), RootState.InitialState, true);

        public App()
        {
            InitializeComponent();

            AppCenter.Start("5f5a9584-4451-4feb-9927-a4f29bb3043d", typeof(Analytics));

            Suspending += OnSuspending;
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var rootFrame = (Window.Current.Content as Frame).ToOption()
                .Match<Frame>()
                .None().Do(() =>
                {
                    var newFrame = CreateNewFrame(e.PreviousExecutionState);
                    Window.Current.Content = newFrame;
                    return newFrame;
                })
                .Some().Do(f => f)
                .Result();

            // Enable router store feature
            Store.EnableRouterFeature(rootFrame);

            // Register Effects
            Store.RegisterEffects(
                TrackAction
            );

            if (!e.PrelaunchActivated)
            {
                var rootFrameContentOption = rootFrame.Content.ToOption();

                if (!rootFrameContentOption.HasValue)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                Window.Current.Activate();
            }
        }

        private Frame CreateNewFrame(ApplicationExecutionState previousExecutionState)
        {
            var newFrame = new Frame();
            newFrame.NavigationFailed += OnNavigationFailed;

            if (previousExecutionState == ApplicationExecutionState.Terminated)
            {
                // TODO : load app state previously suspended
            }

            return newFrame;
        }

        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO : save app state and stop all background tasks

            deferral.Complete();
        }
    }
}
