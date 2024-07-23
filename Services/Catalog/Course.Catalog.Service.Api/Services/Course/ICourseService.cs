using Course.Catalog.Service.Api.Dtos.Course;
using Course.Catalog.Service.Api.Models.Pagination;
using Course.Catalog.Service.Api.Models.Paging;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Course;

public interface ICourseService : IGenericService<CourseDto, Models.Course>
{
    Task<Response<PagedList<CourseWithCategoryDto>>> GetAllWithCategoryAsync(PageRequest request, CancellationToken cancellationToken);
    Task<Response<CourseWithCategoryDto>> GetByIdWithCategory(string id,CancellationToken cancellationToken);
    Task<Response<PagedList<CourseWithCategoryDto>>> GetAllByUserIdWithCategory(string userId,PageRequest request,CancellationToken cancellationToken);
}