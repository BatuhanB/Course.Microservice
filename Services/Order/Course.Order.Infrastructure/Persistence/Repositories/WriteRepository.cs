using Course.Order.Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Course.Order.Infrastructure.Persistence.Repositories;
public class WriteRepository<TEntity, TContext>(TContext context) : IWriteRepository<TEntity>
    where TContext : DbContext
{
    protected TContext Context { get; set; } = context;

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Entry(entity).State = EntityState.Added;
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        return entity;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Context.SaveChangesAsync(cancellationToken);
    }
}