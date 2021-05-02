using System.Collections.Generic;
using System.Linq;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Entity
{
    /// <summary>
    /// Collection of selectors for an <see cref="EntityState{TKey, TEntity}"/>.
    /// </summary>
    /// <typeparam name="TKey">Primary key of the entity.</typeparam>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public class EntitySelectors<TKey, TEntity>
    {
        /// <summary>
        /// Select keys from the state.
        /// </summary>
        public ISelectorWithoutProps<EntityState<TKey, TEntity>, List<TKey>> SelectIds { get; }

        /// <summary>
        /// Select collection (dictionary of entities) from the state.
        /// </summary>
        public ISelectorWithoutProps<EntityState<TKey, TEntity>, Dictionary<TKey, TEntity>> SelectCollection { get; }

        /// <summary>
        /// Select list of entities from the state.
        /// </summary>
        public ISelectorWithoutProps<EntityState<TKey, TEntity>, List<TEntity>> SelectEntities { get; }

        /// <summary>
        /// Select number of entities from the state.
        /// </summary>
        public ISelectorWithoutProps<EntityState<TKey, TEntity>, int> SelectCount { get; }

        internal EntitySelectors(
            IComparer<TEntity> sortComparer = null
        )
        {
            SelectIds = CreateSelector(
                (EntityState<TKey, TEntity> entityState) => entityState.Ids
            );

            SelectCollection = CreateSelector(
                (EntityState<TKey, TEntity> entityState) => entityState.Collection
            );

            SelectEntities = CreateSelector(
                SelectCollection,
                collection =>
                {
                    var result = collection
                        .Select(x => x.Value)
                        .ToList();

                    if (sortComparer != null)
                    {
                        result.Sort(sortComparer);
                    }

                    return result;
                }
            );

            SelectCount = CreateSelector(
                SelectCollection,
                collection => collection.Count
            );
        }
    }

    /// <summary>
    /// Collection of selectors for an <see cref="EntityState{TKey, TEntity}"/>.
    /// </summary>
    /// <typeparam name="TInput">Part of the state used to create selectors.</typeparam>
    /// <typeparam name="TKey">Primary key of the entity.</typeparam>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public class EntitySelectors<TInput, TKey, TEntity>
    {
        /// <summary>
        /// Select keys from the state.
        /// </summary>
        public ISelectorWithoutProps<TInput, List<TKey>> SelectIds { get; }

        /// <summary>
        /// Select collection (dictionary of entities) from the state.
        /// </summary>
        public ISelectorWithoutProps<TInput, Dictionary<TKey, TEntity>> SelectCollection { get; }

        /// <summary>
        /// Select list of entities from the state.
        /// </summary>
        public ISelectorWithoutProps<TInput, List<TEntity>> SelectEntities { get; }

        /// <summary>
        /// Select number of entities from the state.
        /// </summary>
        public ISelectorWithoutProps<TInput, int> SelectCount { get; }

        internal EntitySelectors(
            ISelectorWithoutProps<TInput, EntityState<TKey, TEntity>> selectEntityState,
            IComparer<TEntity> sortComparer = null
        )
        {
            SelectIds = CreateSelector(
                selectEntityState,
                entityState => entityState.Ids
            );

            SelectCollection = CreateSelector(
                selectEntityState,
                entityState => entityState.Collection
            );

            SelectEntities = CreateSelector(
                SelectCollection,
                collection =>
                {
                    var result = collection
                        .Select(x => x.Value)
                        .ToList();

                    if (sortComparer != null)
                    {
                        result.Sort(sortComparer);
                    }

                    return result;
                }
            );

            SelectCount = CreateSelector(
                SelectCollection,
                collection => collection.Count
            );
        }
    }
}
