using System.Collections.Generic;
using System.Linq;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Entity
{
    public class EntitySelectors<TInput, TEntity, TKey>
    {
        public ISelectorWithoutProps<TInput, List<TKey>> SelectIds { get; set; }
        public ISelectorWithoutProps<TInput, Dictionary<TKey, TEntity>> SelectCollection { get; set; }
        public ISelectorWithoutProps<TInput, List<TEntity>> SelectEntities { get; set; }
        public ISelectorWithoutProps<TInput, int> SelectCount { get; set; }

        internal EntitySelectors(
            ISelectorWithoutProps<TInput, EntityState<TEntity, TKey>> selectEntityState,
            IComparer<TEntity> sortComparer
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
