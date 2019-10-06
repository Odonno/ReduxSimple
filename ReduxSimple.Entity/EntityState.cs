using System.Collections.Generic;

namespace ReduxSimple.Entity
{
    /// <summary>
    /// State used to simplify the storage of entities, that can be used with ab <see cref="EntityAdapter{TEntity, TKey}"/>.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class EntityState<TEntity, TKey>
    {
        /// <summary>
        /// List of all the primary keys (ids) in the collection.
        /// </summary>
        public List<TKey> Ids { get; set; } = new List<TKey>();

        /// <summary>
        /// A dictionary of entities in the collection indexed by the primary key.
        /// </summary>
        public Dictionary<TKey, TEntity> Collection { get; set; } = new Dictionary<TKey, TEntity>();
    }
}
