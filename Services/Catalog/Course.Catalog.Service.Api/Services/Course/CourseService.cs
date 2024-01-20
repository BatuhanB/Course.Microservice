using AutoMapper;
using Course.Catalog.Service.Api.Dtos.Category;
using Course.Catalog.Service.Api.Dtos.Course;
using Course.Catalog.Service.Api.Services.Category;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Catalog.Service.Api.Services.Course;

public class CourseService(IMapper mapper, IDatabaseSettings databaseSettings, ICategoryService categoryService)
    : GenericService<Models.Course,CourseDto>(mapper, databaseSettings), ICourseService
{
    private readonly ICategoryService _categoryService = categoryService;
    public async Task<Response<List<CourseWithCategoryDto>>> GetAllWithCategoryAsync(CancellationToken cancellationToken)
    {
        var courses = _mapper.Map<List<CourseWithCategoryDto>>(await _collection.Find(course => true).ToListAsync(cancellationToken)) ;

        if (courses is null) return Response<List<CourseWithCategoryDto>>.Fail("Course Not Found", 404);

        courses = courses.Select(course =>
        {
            var category = _mapper.Map<Response<CategoryDto>>(_categoryService.GetByIdAsync(course.CategoryId, cancellationToken).Result);
            course.Category = category.Data ?? null;
            return course;
        }).ToList();
        
        return Response<List<CourseWithCategoryDto>>.Success(courses, 200);
    }

    public async Task<Response<CourseWithCategoryDto>> GetByIdWithCategory(Guid id, CancellationToken cancellationToken)
    {
        var courses = await _collection.Find(x=>true).ToListAsync(cancellationToken);
        var course = courses.FirstOrDefault(x => x.Id == id);
  
        if (course is null) return Response<CourseWithCategoryDto>.Fail($"No course has been found with {id}", 404);

        course.Category =  _categoryService.GetByIdAsync(course.CategoryId, cancellationToken).Result.Data;
        
        return Response<CourseWithCategoryDto>.Success(_mapper.Map<CourseWithCategoryDto>(course),200);
    }

    public async Task<Response<List<CourseWithCategoryDto>>> GetAllByUserIdWithCategory(Guid userId, CancellationToken cancellationToken)
    {
        var courses = await _collection.Find(x=>true).ToListAsync(cancellationToken);
        courses = courses.Where(x => x.UserId == userId.ToString()).ToList();

        courses = courses.SelectMany(course =>
        {
            var category = _mapper.Map<Response<Models.Category>>(_categoryService.GetByIdAsync(course.CategoryId, cancellationToken).Result);
            course.Category = category.Data ?? null;
            return courses;
        }).ToList();
        
        return Response<List<CourseWithCategoryDto>>.Success(_mapper.Map<List<CourseWithCategoryDto>>(courses), 200);
    }
}