using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Generic;

public interface IGenericService<TResult, TEntity>
{
    Task<Response<List<TResult>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Response<TResult>> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<Response<NoContent>> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<Response<NoContent>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Response<TResult>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}