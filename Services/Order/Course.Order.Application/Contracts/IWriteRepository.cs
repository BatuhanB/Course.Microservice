namespace Course.Order.Application.Contracts;
public interface IWriteRepository<T> : IRepositoryBase<T>
{
    Task<T> AddAsync(T entity,CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}