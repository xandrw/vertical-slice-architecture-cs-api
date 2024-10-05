using Application;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class DataProxy<T>(DatabaseContext context) : IDataProxy<T> where T : class
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

    public async Task<IEnumerable<T>> ReadSqlAsync(FormattableString sql, CancellationToken cancellationToken = default)
    {
        return await context.Set<T>().FromSqlInterpolated(sql).ToListAsync(cancellationToken);
    }

    public async Task<int> WriteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default)
    {
        return await context.Database.ExecuteSqlInterpolatedAsync(sql, cancellationToken);
    }
}