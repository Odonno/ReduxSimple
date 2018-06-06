using System.Collections.Generic;

namespace ReduxSimple.Samples.Pokedex
{
    public class CacheService
    {
        private readonly Dictionary<string, object> _cachedItems = new Dictionary<string, object>();

        public T Get<T>(string key) where T : class
        {
            if (_cachedItems.ContainsKey(key))
            {
                return _cachedItems[key] as T;
            }
            return default(T);
        }

        public void Set<T>(string key, T item)
        {
            _cachedItems[key] = item;
        }
    }
}
