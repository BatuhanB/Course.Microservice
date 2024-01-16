using Course.Catalog.Service.Api.Dtos;
using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Category;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Response<CategoryDto>> CreateAsync(Models.Category category, CancellationToken cancellationToken);

    Task<Response<CategoryDto>> GetById(Guid id, CancellationToken cancellationToken);
}