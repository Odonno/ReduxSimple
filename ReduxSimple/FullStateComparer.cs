using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReduxSimple
{
    internal class FullStateComparer<TState> : IEqualityComparer<TState>
        where TState : class, new()
    {
        private readonly Dictionary<string, IEnumerable<PropertyInfo>> propertiesPerType = new Dictionary<string, IEnumerable<PropertyInfo>>();

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
            if (propertiesPerType.ContainsKey(stateType.FullName))
            {
                return propertiesPerType[stateType.FullName];
            }
            else
            {
                var properties = stateType
                    .GetProperties()
                    .Where(p => p.CanRead && p.CanWrite);

                propertiesPerType.Add(stateType.FullName, properties);

                return properties;
            }
        }
    }
}
