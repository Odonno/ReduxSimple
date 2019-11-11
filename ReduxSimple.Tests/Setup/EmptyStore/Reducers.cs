using System.Collections.Generic;

namespace ReduxSimple.Tests.Setup.EmptyStore
{
    public static class Reducers
    {
        public static IEnumerable<On<EmptyState>> CreateReducers()
        {
            return new List<On<EmptyState>>();
        }
    }
}
