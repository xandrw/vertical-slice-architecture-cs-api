using Domain;

namespace Application.Interfaces;

public interface IDataProxy<T> where T : IEntity
{
    IQueryable<T> Query();
    void Add(T entity);
    void Remove(T entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> ReadSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);
    Task<int> WriteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);
}