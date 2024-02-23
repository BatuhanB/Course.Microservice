using Course.Order.Application.Contracts;
using Course.Order.Application.Contracts.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Course.Order.Infrastructure.Persistence.Repositories;
public class ReadRepository<TEntity, TContext>(TContext context) : IReadRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    protected TContext Context { get; set; } = context;

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                               Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        TEntity? item = include == null
                      ? await Context.Set<TEntity>().FirstOrDefaultAsync(predicate)
                      : await include(Context.Set<TEntity>()).FirstOrDefaultAsync(predicate);

        return item;
    }
    public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null,
                                                       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy =
                                                           null,
                                                       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>?
                                                           include = null,
                                                       int index = 0, int size = 10, bool enableTracking = true,
                                                       CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }
    public IQueryable<TEntity> Query()
    {
        return Context.Set<TEntity>();
    }

}