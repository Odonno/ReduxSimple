using Converto;
using System.Linq;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using static ReduxSimple.Effects;
using static ReduxSimple.Reducers;
using static ReduxSimple.Uwp.RouterStore.Selectors;

namespace ReduxSimple.Uwp.RouterStore
{
    public static class RouterStoreExtensions
    {
        /// <summary>
        /// Enable the router feature to the specified store.
        /// </summary>
        /// <typeparam name="TState">Type of the state.</typeparam>
        /// <param name="store">Store used to store router information.</param>
        /// <param name="rootFrame">The root frame of the UWP application.</param>
        public static void EnableRouterFeature<TState>(this ReduxStore<TState> store, Frame rootFrame)
            where TState : class, IBaseRouterState, new()
        {
            // TODO : Find children frame in the current one for multi-layer navigation?

            // Add router navigation reducers
            var routerReducers = new[]
            {
                 On<RouterNavigatingAction, RouterState>(
                    (state, action) => state.With(new
                    {
                        rootFrame.CanGoBack,
                        rootFrame.CanGoForward,
                    })
                ),
                On<RouterNavigatedAction, RouterState>(
                    (state, action) => state.With(new
                    {
                        rootFrame.CanGoBack,
                        rootFrame.CanGoForward,
                    })
                ),
                On<RouterErrorAction, RouterState>(
                    (state, action) => state.With(new
                    {
                        rootFrame.CanGoBack,
                        rootFrame.CanGoForward,
                    })
                ),
                On<RouterCancelAction, RouterState>(
                    (state, action) => state.With(new
                    {
                        rootFrame.CanGoBack,
                        rootFrame.CanGoForward,
                    })
                )
            };

            var reducers = CreateSubReducers<TState, RouterState>(routerReducers, SelectRouter);
            store.AddReducers(reducers);

            // Listen to router events
            var navigatingEffect = CreateEffect<TState>(
                () => rootFrame.Events().Navigating
                    .Select(@event =>
                    {
                        return new RouterNavigatingAction
                        {
                            Event = new RouterNavigatingEvent
                            {
                                Cancel = @event.Cancel,
                                NavigationMode = @event.NavigationMode,
                                Parameter = @event.Parameter,
                                SourcePageType = @event.SourcePageType
                            }
                        };
                    }),
                true
            );
            var navigatedEffect = CreateEffect<TState>(
                () => rootFrame.Events().Navigated
                    .Select(@event =>
                    {
                        return new RouterNavigatedAction
                        {
                            Event = new RouterNavigatedEvent
                            {
                                ContentType = @event.Content?.GetType(),
                                SourcePageType = @event.SourcePageType,
                                NavigationMode = @event.NavigationMode,
                                Parameter = @event.Parameter,
                                Uri = @event.Uri
                            }
                        };
                    }),
                true
            );
            var navigationFailedEffect = CreateEffect<TState>(
                () => rootFrame.Events().NavigationFailed
                    .Select(@event =>
                    {
                        return new RouterErrorAction
                        {
                            Event = new RouterErrorEvent
                            {
                                Exception = @event.Exception,
                                Handled = @event.Handled,
                                SourcePageType = @event.SourcePageType
                            }
                        };
                    }),
                true
            );
            var navigationStoppedEffect = CreateEffect<TState>(
                () => rootFrame.Events().NavigationStopped
                    .Select(@event =>
                    {
                        return new RouterCancelAction
                        {
                            Event = new RouterCancelEvent
                            {
                                ContentType = @event.Content?.GetType(),
                                SourcePageType = @event.SourcePageType,
                                NavigationMode = @event.NavigationMode,
                                Parameter = @event.Parameter,
                                Uri = @event.Uri
                            }
                        };
                    }),
                true
            );

            // Add router navigation effects
            store.RegisterEffects(
                navigatingEffect,
                navigatedEffect,
                navigationFailedEffect,
                navigationStoppedEffect
            );
        }
    }
}
