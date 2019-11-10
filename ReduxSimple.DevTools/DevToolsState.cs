using System.Collections.Immutable;

namespace ReduxSimple.DevTools
{
    public class DevToolsState
    {
        public ImmutableList<ReduxActionInfo> CurrentActions { get; set; } = ImmutableList<ReduxActionInfo>.Empty;
        public ImmutableList<ReduxActionInfo> FutureActions { get; set; } = ImmutableList<ReduxActionInfo>.Empty;
        public int SelectedActionPosition { get; set; } = 0;
        public bool PlaySessionActive { get; set; } = false;
    }
}
