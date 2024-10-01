namespace Domain;

public interface IRepository<T> where T : class
{
    IQueryable<T> Query();
    void Add(T entity);
    void Remove(T entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}