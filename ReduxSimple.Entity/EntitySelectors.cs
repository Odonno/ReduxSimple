using System.Collections.Generic;
using System.Linq;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Entity
{
    /// <summary>
    /// Collection of selectors for an <see cref="EntityState{TEntity, TKey}"/>.
    /// </summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <typeparam name="TKey">Primary key of the entity.</typeparam>
    public class EntitySelectors<TEntity, TKey>
    {
        /// <summary>
        /// Select keys from the state.
        /// </summary>
        public ISelectorWithoutProps<EntityState<TEntity, TKey>, List<TKey>> SelectIds { get; }

        /// <summary>
        /// Select collection (dictionary of entities) from the state.
        /// </summary>
        public ISelectorWithoutProps<EntityState<TEntity, TKey>, Dictionary<TKey, TEntity>> SelectCollection { get; }

        /// <summary>
        /// Select list of entities from the state.
        /// </summary>
        public ISelectorWithoutProps<EntityState<TEntity, TKey>, List<TEntity>> SelectEntities { get; }

        /// <summary>
        /// Select number of entities from the state.
        /// </summary>
        public ISelectorWithoutProps<EntityState<TEntity, TKey>, int> SelectCount { get; }

        internal EntitySelectors(
            IComparer<TEntity>? sortComparer
        )
        {
            SelectIds = CreateSelector(
                (EntityState<TEntity, TKey> entityState) => entityState.Ids
            );

            SelectCollection = CreateSelector(
                (EntityState<TEntity, TKey> entityState) => entityState.Collection
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
    /// Collection of selectors for an <see cref="EntityState{TEntity, TKey}"/>.
    /// </summary>
    /// <typeparam name="TInput">Part of the state used to create selectors.</typeparam>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    /// <typeparam name="TKey">Primary key of the entity.</typeparam>
    public class EntitySelectors<TInput, TEntity, TKey>
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
            ISelectorWithoutProps<TInput, EntityState<TEntity, TKey>> selectEntityState,
            IComparer<TEntity>? sortComparer
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
