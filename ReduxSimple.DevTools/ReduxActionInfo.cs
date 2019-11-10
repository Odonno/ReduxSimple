using System;

namespace ReduxSimple.DevTools
{
    /// <summary>
    /// Redux action details used in the Redux DevTools.
    /// </summary>
    public class ReduxActionInfo
    {
        /// <summary>
        /// The date when the action was dispatched.
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// The type of the action.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The payload of the action.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// The state before the action was dispatched in the store.
        /// </summary>
        public object PreviousState { get; set; }

        /// <summary>
        /// The state after the action was dispatched in the store.
        /// </summary>
        public object NextState { get; set; }
    }
}
