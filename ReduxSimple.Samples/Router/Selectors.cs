using System;

namespace ReduxSimple.Samples.Router
{
    public static class Selectors
    {
        public static Func<IBaseRouterState, RouterState> SelectRouter = state => state.Router;
    }
}
