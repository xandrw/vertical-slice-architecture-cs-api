using Domain;

namespace Infrastructure.Persistence;

public class Repository<T>(DatabaseContext context) : IRepository<T> where T : class
{
    public IQueryable<T> Query()
    {
        return context.Set<T>().AsQueryable();
    }

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}