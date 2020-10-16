namespace ReduxSimple
{
    internal abstract class ActionDispatched
    {
        public object Action { get; }

        protected ActionDispatched(object action)
        {
            Action = action;
        }
    }

    internal sealed class ActionDispatchedWithOrigin : ActionDispatched
    {
        public ActionOrigin Origin { get; }

        public ActionDispatchedWithOrigin(object action, ActionOrigin origin) : base(action)
        {
            Origin = origin;
        }
    }

    internal sealed class ActionDispatchedWithRewriteHistory : ActionDispatched
    {
        public bool RewriteHistory { get; }

        public ActionDispatchedWithRewriteHistory(object action, bool rewriteHistory) : base(action)
        {
            RewriteHistory = rewriteHistory;
        }
    }
}
