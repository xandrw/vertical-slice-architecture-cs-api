namespace Application;

public interface IDataProxy<T> where T : class
{
    IQueryable<T> Query();
    void Add(T entity);
    void Remove(T entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> ReadSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);
    Task<int> WriteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);
}