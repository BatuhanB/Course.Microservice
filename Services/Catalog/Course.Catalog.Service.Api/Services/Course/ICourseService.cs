using Course.Catalog.Service.Api.Dtos.Course;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Course;

public interface ICourseService : IGenericService<CourseDto, Models.Course>
{
    Task<Response<List<CourseWithCategoryDto>>> GetAllWithCategoryAsync(CancellationToken cancellationToken);
    Task<Response<CourseWithCategoryDto>> GetByIdWithCategory(Guid id,CancellationToken cancellationToken);
    Task<Response<List<CourseWithCategoryDto>>> GetAllByUserIdWithCategory(Guid userId,CancellationToken cancellationToken);
}