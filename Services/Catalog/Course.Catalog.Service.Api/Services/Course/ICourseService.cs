using Course.Catalog.Service.Api.Dtos;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Course;

public interface ICourseService : IGenericService<Models.Course>
{
    Task<Response<List<CourseDto>>> GetAllWithCategoryAsync(CancellationToken cancellationToken);
}