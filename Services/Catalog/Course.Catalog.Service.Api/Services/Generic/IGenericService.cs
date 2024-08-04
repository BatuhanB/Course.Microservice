using Res = Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Generic;

public interface IGenericService<TResult, TEntity>
{
    Task<Res.Response<List<TResult>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Res.Response<TResult>> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<Res.Response<Res.NoContent>> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<Res.Response<Res.NoContent>> DeleteAsync(string id, CancellationToken cancellationToken);
    Task<Res.Response<TResult>> GetByIdAsync(string id, CancellationToken cancellationToken);
}