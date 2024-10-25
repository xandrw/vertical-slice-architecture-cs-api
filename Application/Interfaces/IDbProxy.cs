using Domain;

namespace Application.Interfaces;

public interface IDbProxy<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> Query();
    void Add(TEntity entity);
    void Remove(TEntity entity);
    /** /Caution\: Update marks all TEntity.properties as modified and can cause SQL performance issues */
    void Update(TEntity entity); 
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> ReadSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);
    Task<int> WriteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);
}