using Converto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReduxSimple.Entity
{
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

        public TEntityState UpsertOne<TEntityState>(TEntity entity, TEntityState state)
            where TEntityState : EntityState<TEntity, TKey>
        {
            return UpsertMany(new[] { entity }, state);
        }
        // TODO : UpsertOne with Partial<TEntity>

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

        public TEntityState RemoveOne<TEntityState>(TKey key, TEntityState state)
            where TEntityState : EntityState<TEntity, TKey>
        {
            return RemoveMany(new[] { key }, state);
        }

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

    public sealed class EntityAdapter<TEntity, TKey> : EntityStateAdapter<TEntity, TKey>
    {
        private EntityAdapter()
        {
        }

        public EntitySelectors<TInput, TEntity, TKey> GetSelectors<TInput>(
            ISelectorWithoutProps<TInput, EntityState<TEntity, TKey>> selectEntityState
        )
        {
            return new EntitySelectors<TInput, TEntity, TKey>(selectEntityState, SortComparer);
        }

        public static EntityAdapter<TEntity, TKey> Create(Func<TEntity, TKey> selectId, IComparer<TEntity> sortComparer = null)
        {
            return new EntityAdapter<TEntity, TKey>
            {
                SelectId = selectId,
                SortComparer = sortComparer
            };
        }
    }
}
