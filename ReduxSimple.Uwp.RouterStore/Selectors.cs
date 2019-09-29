using static ReduxSimple.Selectors;

namespace ReduxSimple.Uwp.RouterStore
{
    public static class Selectors
    {
        public static ISelectorWithoutProps<IBaseRouterState, RouterState> SelectRouter = CreateSelector(
            (IBaseRouterState state) => state.Router
        );

        public static ISelectorWithoutProps<IBaseRouterState, bool> SelectCanGoBack = CreateSelector(
            SelectRouter,
            router => router.CanGoBack
        );
        public static ISelectorWithoutProps<IBaseRouterState, bool> SelectCanGoForward = CreateSelector(
            SelectRouter,
            router => router.CanGoForward
        );
    }
}
