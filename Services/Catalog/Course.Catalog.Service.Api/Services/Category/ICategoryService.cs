using Course.Catalog.Service.Api.Dtos.Category;
using Course.Catalog.Service.Api.Services.Generic;

namespace Course.Catalog.Service.Api.Services.Category;

public interface ICategoryService : IGenericService<Models.Category,CategoryDto>
{
}