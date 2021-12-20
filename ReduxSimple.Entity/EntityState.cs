namespace ReduxSimple.Entity;

/// <summary>
/// State used to simplify the storage of entities, that can be used with ab <see cref="EntityAdapter{TKey, TEntity}"/>.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public abstract record EntityState<TKey, TEntity>
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
