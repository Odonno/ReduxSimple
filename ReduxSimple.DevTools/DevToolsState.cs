using System.Collections.Immutable;

namespace ReduxSimple.DevTools
{
    /// <summary>
    /// State used (internally) to display information about the Application Store.
    /// </summary>
    public class DevToolsState
    {
        /// <summary>
        /// List of current (dispatched) actions in the Application Store.
        /// </summary>
        public ImmutableList<ReduxActionInfo> CurrentActions { get; set; } = ImmutableList<ReduxActionInfo>.Empty;

        /// <summary>
        /// List of undone actions in the Application Store.
        /// </summary>
        public ImmutableList<ReduxActionInfo> FutureActions { get; set; } = ImmutableList<ReduxActionInfo>.Empty;

        /// <summary>
        /// Index of the selected action in the list of current (dispatched) actions.
        /// </summary>
        public int SelectedActionPosition { get; set; } = 0;

        /// <summary>
        /// Activated to play/pause store to forward redone actions.
        /// </summary>
        public bool PlaySessionActive { get; set; } = false;
    }
}
