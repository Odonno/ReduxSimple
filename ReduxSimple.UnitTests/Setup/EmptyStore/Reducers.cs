using System.Collections.Generic;

namespace ReduxSimple.UnitTests.Setup.EmptyStore
{
    public static class Reducers
    {
        public static IEnumerable<On<EmptyState>> CreateReducers()
        {
            return new List<On<EmptyState>>();
        }
    }
}
