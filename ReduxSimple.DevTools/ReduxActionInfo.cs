using System;

namespace ReduxSimple.DevTools
{
    public class ReduxActionInfo
    {
        public DateTime? Date { get; set; }
        public Type Type { get; set; }
        public object Data { get; set; }
        public object PreviousState { get; set; }
        public object NextState { get; set; }
    }
}
