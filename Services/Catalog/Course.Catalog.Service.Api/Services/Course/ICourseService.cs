using Course.Catalog.Service.Api.Dtos;
using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Course;

public interface ICourseService
{
    Task<Response<List<CourseDto>>> GetAllAsync(CancellationToken cancellationToken);
}