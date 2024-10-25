using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class DbProxy<TEntity>(DatabaseContext context) : IDbProxy<TEntity> where TEntity : class, IEntity
{
    public IQueryable<TEntity> Query()
    {
        return context.Set<TEntity>().AsQueryable();
    }

    public void Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
    }

    public void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> ReadSqlAsync(
        FormattableString sql,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().FromSqlInterpolated(sql).ToListAsync(cancellationToken);
    }

    public async Task<int> WriteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default)
    {
        return await context.Database.ExecuteSqlInterpolatedAsync(sql, cancellationToken);
    }
}