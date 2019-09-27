using System;

namespace ReduxSimple.Uwp.RouterStore
{
    public static class Selectors
    {
        public static Func<IBaseRouterState, RouterState> SelectRouter = state => state.Router;
    }
}
