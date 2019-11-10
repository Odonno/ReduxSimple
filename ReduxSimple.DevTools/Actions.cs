using System.Collections.Immutable;

namespace ReduxSimple.DevTools
{
    public class HistoryUpdated
    {
        public ImmutableList<ReduxActionInfo> CurrentActions { get; set; }
        public ImmutableList<ReduxActionInfo> FutureActions { get; set; }
    }
    
    public class MoveToPositionAction
    {
        public int Position { get; set; }
    }
    
    public class SelectPositionAction
    {
        public int Position { get; set; }
    }

    public class TogglePlayPauseAction { }
}
