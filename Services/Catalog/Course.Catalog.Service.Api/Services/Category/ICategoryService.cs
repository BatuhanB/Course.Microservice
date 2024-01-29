using Course.Catalog.Service.Api.Dtos.Category;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Category;

public interface ICategoryService : IGenericService<CategoryDto, Models.Category>
{
    public Task<Response<List<CategoryWithCoursesDto>>> GetAllCategoryWithCourses(CancellationToken cancellationToken);
}