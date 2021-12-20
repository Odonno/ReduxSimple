namespace ReduxSimple
{
    internal abstract class ActionDispatched
    {
        public object Action { get; }
        
        public object StateWhenDispatched { get; }

        protected ActionDispatched(object action, object state)
        {
            Action = action;
            StateWhenDispatched = state;
        }
    }

    internal sealed class ActionDispatchedWithOrigin : ActionDispatched
    {
        public ActionOrigin Origin { get; }
        
        public ActionDispatchedWithOrigin(object action, object state, ActionOrigin origin) 
            : base(action, state)
        {
            Origin = origin;
        }
    }

    internal sealed class ActionDispatchedWithRewriteHistory : ActionDispatched
    {
        public bool RewriteHistory { get; }

        public ActionDispatchedWithRewriteHistory(object action, object state, bool rewriteHistory) 
            : base(action, state)
        {
            RewriteHistory = rewriteHistory;
        }
    }
}
