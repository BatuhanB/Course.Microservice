using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Generic;

public interface IGenericService<TEntity,TDto>
{
    Task<Response<List<TEntity>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Response<TEntity>> CreateAsync(TDto dto, CancellationToken cancellationToken);
    Task<Response<NoContent>> UpdateAsync(TDto dto, CancellationToken cancellationToken);
    Task<Response<NoContent>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Response<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}