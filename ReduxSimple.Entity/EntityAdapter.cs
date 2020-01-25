using System;
using System.Collections.Generic;

namespace ReduxSimple.Entity
{
    /// <summary>
    /// Adapter of an <see cref="EntityState{TEntity, TKey}"/> with the different reducer functions to handle entity manipulation.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <typeparam name="TKey">Primary key of the entity.</typeparam>
    public sealed class EntityAdapter<TEntity, TKey> : EntityStateAdapter<TEntity, TKey>
        where TEntity : class
    {
        private EntityAdapter()
        {
        }

        /// <summary>
        /// Get selectors for the specified <see cref="EntityState{TEntity, TKey}"/>.
        /// </summary>
        /// <returns>A new Entity Selectors.</returns>
        public EntitySelectors<TEntity, TKey> GetSelectors()
        {
            return new EntitySelectors<TEntity, TKey>(SortComparer);
        }
        /// <summary>
        /// Get selectors for the specified <see cref="EntityState{TEntity, TKey}"/>.
        /// </summary>
        /// <typeparam name="TInput">Part of the state used to create selectors.</typeparam>
        /// <param name="selectEntityState">Function used to select <see cref="EntityState{TEntity, TKey}"/> from the <typeparamref name="TInput"/>.</param>
        /// <returns>A new Entity Selectors.</returns>
        public EntitySelectors<TInput, TEntity, TKey> GetSelectors<TInput>(
            ISelectorWithoutProps<TInput, EntityState<TEntity, TKey>> selectEntityState
        )
        {
            return new EntitySelectors<TInput, TEntity, TKey>(selectEntityState, SortComparer);
        }

        /// <summary>
        /// Creates a new <see cref="EntityAdapter{TEntity, TKey}"/>.
        /// </summary>
        /// <param name="selectId">Function used to get the id of an entity.</param>
        /// <param name="sortComparer">Comparer used to sort the collection of entities.</param>
        /// <returns>A new Entity Adapter.</returns>
        public static EntityAdapter<TEntity, TKey> Create(Func<TEntity, TKey> selectId, IComparer<TEntity>? sortComparer = null)
        {
            return new EntityAdapter<TEntity, TKey>
            {
                SelectId = selectId,
                SortComparer = sortComparer
            };
        }
    }
}
