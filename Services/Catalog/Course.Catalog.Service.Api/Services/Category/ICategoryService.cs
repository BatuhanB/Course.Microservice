using Course.Catalog.Service.Api.Dtos.Category;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Category;

public interface ICategoryService : IGenericService<Models.Category,CategoryDto>
{
    public Task<Response<List<CategoryWithCoursesDto>>> GetAllCategoryWithCourses(CancellationToken cancellationToken);
}