using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Generic;

public interface IGenericService<TEntity>
{
    Task<Response<List<TEntity>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Response<TEntity>> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<Response<NoContent>> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<Response<NoContent>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Response<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}