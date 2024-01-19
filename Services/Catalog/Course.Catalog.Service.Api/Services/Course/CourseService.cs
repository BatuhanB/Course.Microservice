using AutoMapper;
using Course.Catalog.Service.Api.Dtos;
using Course.Catalog.Service.Api.Services.Category;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Catalog.Service.Api.Services.Course;

public class CourseService(IMapper mapper, IDatabaseSettings databaseSettings, ICategoryService categoryService)
    : GenericService<Models.Course>(mapper, databaseSettings), ICourseService
{
    private readonly ICategoryService _categoryService = categoryService;
    public async Task<Response<List<CourseDto>>> GetAllWithCategoryAsync(CancellationToken cancellationToken)
    {
        var courses = _mapper.Map<List<CourseDto>>(await _collection.Find(course => true).ToListAsync(cancellationToken)) ;

        if (courses is null) return Response<List<CourseDto>>.Fail("Course Not Found", 404);

        courses = courses.SelectMany(course =>
        {
            var category = _mapper.Map<Response<CategoryDto>>(_categoryService.GetByIdAsync(course.CategoryId, cancellationToken).Result);
            course.Category = category.Data ?? null;
            return courses;
        }).ToList();
        
        return Response<List<CourseDto>>.Success(courses, 200);
    }

    public async Task<Response<CourseDto>> GetByIdWithCategory(Guid id, CancellationToken cancellationToken)
    {
        var course = _mapper.Map<CourseDto>(await _collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken));

        if (course is null) return Response<CourseDto>.Fail($"No course has been found with {id}", 404);

        course.Category = _mapper.Map<CategoryDto>(await _categoryService.GetByIdAsync(course.CategoryId, cancellationToken));
        
        return Response<CourseDto>.Success(course,200);
    }

    public async Task<Response<List<CourseDto>>> GetAllByUserIdWithCategory(Guid userId, CancellationToken cancellationToken)
    {
        var courses = _mapper.Map<List<CourseDto>>(await _collection.Find(x=>x.UserId == userId.ToString()).ToListAsync(cancellationToken)) ;

        if (courses is null) return Response<List<CourseDto>>.Fail("Course Not Found", 404);

        courses = courses.SelectMany(course =>
        {
            var category = _mapper.Map<Response<CategoryDto>>(_categoryService.GetByIdAsync(course.CategoryId, cancellationToken).Result);
            course.Category = category.Data ?? null;
            return courses;
        }).ToList();
        
        return Response<List<CourseDto>>.Success(courses, 200);
    }
}