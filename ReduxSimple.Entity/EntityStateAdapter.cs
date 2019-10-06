using Converto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReduxSimple.Entity
{
    /// <summary>
    /// Adapter of an <see cref="EntityState{TEntity, TKey}"/> with the different reducer functions to handle entity manipulation.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <typeparam name="TKey">Primary key of the entity.</typeparam>
    public abstract class EntityStateAdapter<TEntity, TKey>
    {
        /// <summary>
        /// Function used to get the primary key of the entity.
        /// </summary>
        public Func<TEntity, TKey> SelectId { get; internal set; }

        /// <summary>
        /// Sort function to get entities custom sort.
        /// </summary>
        public IComparer<TEntity> SortComparer { get; internal set; }

        /// <summary>
        /// Add (reset and add) all entities in the state.
        /// </summary>
        /// <typeparam name="TEntityState">Type of the Entity State.</typeparam>
        /// <param name="entities">List of entities to add.</param>
        /// <param name="state">Current Entity State.</param>
        /// <returns>Updated Entity State.</returns>
        public TEntityState AddAll<TEntityState>(IEnumerable<TEntity> entities, TEntityState state)
            where TEntityState : EntityState<TEntity, TKey>
        {
            var collection = entities.ToDictionary(SelectId);
            return state.With(new
            {
                Ids = collection.Keys.ToList(),
                Collection = collection
            });
        }

        /// <summary>
        /// Upsert (add or update) an entity in the state.
        /// </summary>
        /// <typeparam name="TEntityState">Type of the Entity State.</typeparam>
        /// <param name="entity">Entity to upsert.</param>
        /// <param name="state">Current Entity State.</param>
        /// <returns>Updated Entity State.</returns>
        public TEntityState UpsertOne<TEntityState>(TEntity entity, TEntityState state)
            where TEntityState : EntityState<TEntity, TKey>
        {
            return UpsertMany(new[] { entity }, state);
        }
        // TODO : UpsertOne with Partial<TEntity>

        /// <summary>
        /// Upsert (add or update) multiple entities in the state.
        /// </summary>
        /// <typeparam name="TEntityState">Type of the Entity State.</typeparam>
        /// <param name="entities">List of entities to upsert.</param>
        /// <param name="state">Current Entity State.</param>
        /// <returns>Updated Entity State.</returns>
        public TEntityState UpsertMany<TEntityState>(IEnumerable<TEntity> entities, TEntityState state)
            where TEntityState : EntityState<TEntity, TKey>
        {
            var keys = entities.Select(SelectId);

            var currentEntities = state.Collection.Values;
            var collection = currentEntities
                .Except(currentEntities.Where(e => keys.Contains(SelectId(e))))
                .Concat(entities)
                .ToDictionary(SelectId);

            return state.With(new
            {
                Ids = collection.Keys.ToList(),
                Collection = collection
            });
        }
        // TODO : UpsertMany with Partial<TEntity>

        /// <summary>
        /// Remove one entity from the state.
        /// </summary>
        /// <typeparam name="TEntityState">Type of the Entity State.</typeparam>
        /// <param name="key">Key of the entity to remove.</param>
        /// <param name="state">Current Entity State.</param>
        /// <returns>Updated Entity State.</returns>
        public TEntityState RemoveOne<TEntityState>(TKey key, TEntityState state)
            where TEntityState : EntityState<TEntity, TKey>
        {
            return RemoveMany(new[] { key }, state);
        }

        /// <summary>
        /// Remove multiple entities from the state.
        /// </summary>
        /// <typeparam name="TEntityState">Type of the Entity State.</typeparam>
        /// <param name="keys">List of keys of the entities to remove.</param>
        /// <param name="state">Current Entity State.</param>
        /// <returns>Updated Entity State.</returns>
        public TEntityState RemoveMany<TEntityState>(IEnumerable<TKey> keys, TEntityState state)
            where TEntityState : EntityState<TEntity, TKey>
        {
            var entities = state.Collection.Values;
            var collection = entities
                .Except(entities.Where(e => keys.Contains(SelectId(e))))
                .ToDictionary(SelectId);

            return state.With(new
            {
                Ids = collection.Keys.ToList(),
                Collection = collection
            });
        }

        /// <summary>
        /// Remove all entities from the state, to get a clean state.
        /// </summary>
        /// <typeparam name="TEntityState">Type of the Entity State.</typeparam>
        /// <param name="state">Current Entity State.</param>
        /// <returns>Updated Entity State.</returns>
        public TEntityState RemoveAll<TEntityState>(TEntityState state)
            where TEntityState : EntityState<TEntity, TKey>
        {
            var collection = new Dictionary<TEntity, TKey>();
            return state.With(new
            {
                Ids = collection.Keys.ToList(),
                Collection = collection
            });
        }

        /// <summary>
        /// Update entities in the state using a <paramref name="map"/> function.
        /// </summary>
        /// <typeparam name="TEntityState">Type of the Entity State.</typeparam>
        /// <param name="map">Map function used to update an entity.</param>
        /// <param name="state">Current Entity State.</param>
        /// <returns>Updated Entity State.</returns>
        public TEntityState Map<TEntityState>(Func<TEntity, TEntity> map, TEntityState state)
            where TEntityState : EntityState<TEntity, TKey>
        {
            var entities = state.Collection.Values.Select(map);
            var collection = entities.ToDictionary(SelectId);

            return state.With(new
            {
                Ids = collection.Keys.ToList(),
                Collection = collection
            });
        }
    }
}
