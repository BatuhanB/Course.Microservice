using AutoMapper;
using Course.Catalog.Service.Api.Dtos.Category;
using Course.Catalog.Service.Api.Dtos.Course;
using Course.Catalog.Service.Api.Models;
using Course.Catalog.Service.Api.Services.Category;
using Course.Catalog.Service.Api.Services.Generic;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Catalog.Service.Api.Services.Course;

public class CourseService(IMapper mapper, IDatabaseSettings databaseSettings, ICategoryService categoryService)
    : GenericService<CourseDto, Models.Course>(mapper, databaseSettings), ICourseService
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

    public async Task<Response<CourseWithCategoryDto>> GetByIdWithCategory(string id, CancellationToken cancellationToken)
    {
        var courses = await _collection.Find(x=>true).ToListAsync(cancellationToken);
        var course = courses.FirstOrDefault(x => x.Id == id);
  
        if (course is null) return Response<CourseWithCategoryDto>.Fail($"No course has been found with {id}", 404);

        course.Category =  _mapper.Map<Models.Category>(_categoryService.GetByIdAsync(course.CategoryId, cancellationToken).Result.Data);
        
        return Response<CourseWithCategoryDto>.Success(_mapper.Map<CourseWithCategoryDto>(course),200);
    }

    public async Task<Response<List<CourseWithCategoryDto>>> GetAllByUserIdWithCategory(string userId, CancellationToken cancellationToken)
    {
        var courses = await _collection.Find(x => x.UserId == userId.ToString()).ToListAsync(cancellationToken);

        var tasks = courses.Select(async course =>
        {
            var categoryResponse = await _categoryService.GetByIdAsync(course.CategoryId, cancellationToken);
            course.Category = _mapper.Map<Models.Category>(categoryResponse.Data);
            return course;
        }).ToList();

        var completedResult = await Task.WhenAll(tasks);

        return Response<List<CourseWithCategoryDto>>.Success(_mapper.Map<List<CourseWithCategoryDto>>(completedResult.ToList()), 200);
    }
}