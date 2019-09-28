using static ReduxSimple.Selectors;

namespace ReduxSimple.Uwp.RouterStore
{
    public static class Selectors
    {
        public static ISelectorWithoutProps<IBaseRouterState, RouterState> SelectRouter = CreateSelector(
            (IBaseRouterState state) => state.Router
        );
    }
}
