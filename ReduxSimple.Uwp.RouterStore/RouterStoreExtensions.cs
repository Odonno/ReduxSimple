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
        public static void EnableRouterFeature<TState>(this ReduxStore<TState> store, Frame rootFrame)
            where TState : class, IBaseRouterState, new()
        {
            // TODO : Find children frame in the current one for multi-layer navigation?

            // Add router navigation reducers
            var routerReducers = new[]
            {
                 On<RouterNavigating, RouterState>(
                    (state, action) => state.With(new
                    {
                        rootFrame.CanGoBack,
                        rootFrame.CanGoForward,
                    })
                ),
                On<RouterNavigated, RouterState>(
                    (state, action) => state.With(new
                    {
                        rootFrame.CanGoBack,
                        rootFrame.CanGoForward,
                    })
                ),
                On<RouterError, RouterState>(
                    (state, action) => state.With(new
                    {
                        rootFrame.CanGoBack,
                        rootFrame.CanGoForward,
                    })
                ),
                On<RouterCancel, RouterState>(
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
                        return new RouterNavigating
                        {
                            Event = @event
                        };
                    }),
                true
            );
            var navigatedEffect = CreateEffect<TState>(
                () => rootFrame.Events().Navigated
                    .Select(@event =>
                    {
                        return new RouterNavigated
                        {
                            Event = @event
                        };
                    }),
                true
            );
            var navigationFailedEffect = CreateEffect<TState>(
                () => rootFrame.Events().NavigationFailed
                    .Select(@event =>
                    {
                        return new RouterError
                        {
                            Event = @event
                        };
                    }),
                true
            );
            var navigationStoppedEffect = CreateEffect<TState>(
                () => rootFrame.Events().NavigationStopped
                    .Select(@event =>
                    {
                        return new RouterCancel
                        {
                            Event = @event
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
