namespace ReduxSimple.Entity;

/// <summary>
/// Adapter of an <see cref="EntityState{TKey, TEntity}"/> with the different reducer functions to handle entity manipulation.
/// </summary>
/// <typeparam name="TKey">Primary key of the entity.</typeparam>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
public sealed class EntityAdapter<TKey, TEntity> : EntityStateAdapter<TKey, TEntity>
    where TEntity : class
{
    private EntityAdapter()
    {
    }

    /// <summary>
    /// Get selectors for the specified <see cref="EntityState{TKey, TEntity}"/>.
    /// </summary>
    /// <returns>A new Entity Selectors.</returns>
    public EntitySelectors<TKey, TEntity> GetSelectors()
    {
        return new EntitySelectors<TKey, TEntity>(SortComparer);
    }
    /// <summary>
    /// Get selectors for the specified <see cref="EntityState{TKey, TEntity}"/>.
    /// </summary>
    /// <typeparam name="TInput">Part of the state used to create selectors.</typeparam>
    /// <param name="selectEntityState">Function used to select <see cref="EntityState{TKey, TEntity}"/> from the <typeparamref name="TInput"/>.</param>
    /// <returns>A new Entity Selectors.</returns>
    public EntitySelectors<TInput, TKey, TEntity> GetSelectors<TInput>(
        ISelectorWithoutProps<TInput, EntityState<TKey, TEntity>> selectEntityState
    )
    {
        return new EntitySelectors<TInput, TKey, TEntity>(selectEntityState, SortComparer);
    }

    /// <summary>
    /// Creates a new <see cref="EntityAdapter{TKey, TEntity}"/>.
    /// </summary>
    /// <param name="selectId">Function used to get the id of an entity.</param>
    /// <param name="sortComparer">Comparer used to sort the collection of entities.</param>
    /// <returns>A new Entity Adapter.</returns>
    public static EntityAdapter<TKey, TEntity> Create(Func<TEntity, TKey> selectId, IComparer<TEntity> sortComparer = null)
    {
        return new EntityAdapter<TKey, TEntity>
        {
            SelectId = selectId,
            SortComparer = sortComparer
        };
    }
}
