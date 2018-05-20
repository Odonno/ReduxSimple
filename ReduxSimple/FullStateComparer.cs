using System.Collections.Generic;
using System.Linq;

namespace ReduxSimple
{
    internal class FullStateComparer<TState> : IEqualityComparer<TState>
        where TState : class, new()
    {
        public bool Equals(TState x, TState y)
        {
            var stateType = typeof(TState);

            var properties = stateType
                .GetProperties()
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var property in properties)
            {
                var leftValue = property.GetValue(x, null);
                var rightValue = property.GetValue(y, null);

                if (leftValue != rightValue)
                    return false;
            }
            
            return true;
        }

        public int GetHashCode(TState obj)
        {
            return obj.GetHashCode();
        }
    }
}
