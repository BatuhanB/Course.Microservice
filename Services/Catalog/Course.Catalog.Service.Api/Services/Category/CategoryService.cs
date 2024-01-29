using AutoMapper;
using Course.Catalog.Service.Api.Dtos.Category;
using Course.Catalog.Service.Api.Dtos.Course;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Catalog.Service.Api.Services.Category;

public class CategoryService(IMapper mapper, IDatabaseSettings databaseSettings,IGenericService<CourseDto, Models.Course> courseCollectionService)
    : GenericService<CategoryDto, Models.Category>(mapper, databaseSettings), ICategoryService
{
    private readonly IGenericService<CourseDto, Models.Course> _courseCollectionService = courseCollectionService;

    public async Task<Response<List<CategoryWithCoursesDto>>> GetAllCategoryWithCourses(CancellationToken cancellationToken)
    {
        var categories = _mapper.Map<List<CategoryWithCoursesDto>>(await _collection.Find(x => true).ToListAsync(cancellationToken));

        categories = categories.Select(category =>
        {
            var resultData = _courseCollectionService.GetAllAsync(cancellationToken).Result.Data;
            if (resultData is not null)
            {
                category.Courses = _mapper.Map<List<CourseDto>>(resultData
                    .Where(course => course.CategoryId == category.Id).ToList());
            }

            return category;
        }).ToList();

        return Response<List<CategoryWithCoursesDto>>.Success(categories, 200);
    }
}