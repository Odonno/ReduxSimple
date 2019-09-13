using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReduxSimple
{
    internal class FullStateComparer<TState> : IEqualityComparer<TState>
        where TState : class, new()
    {
        private readonly ConcurrentDictionary<string, IEnumerable<PropertyInfo>> _propertiesPerType = new ConcurrentDictionary<string, IEnumerable<PropertyInfo>>();

        public bool Equals(TState x, TState y)
        {
            var stateType = typeof(TState);

            var properties = GetStatePropertiesFromCache(stateType);

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

        private IEnumerable<PropertyInfo> GetStatePropertiesFromCache(Type stateType)
        {
            return _propertiesPerType.GetOrAdd(
                stateType.FullName,
                _ =>
                {
                    return stateType
                        .GetProperties()
                        .Where(p => p.CanRead && p.CanWrite);
                }
            );
        }
    }
}
