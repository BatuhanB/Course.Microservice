using Amazon.Runtime.Internal;
using AutoMapper;
using Course.Catalog.Service.Api.Dtos.Category;
using Course.Catalog.Service.Api.Dtos.Course;
using Course.Catalog.Service.Api.Models;
using Course.Catalog.Service.Api.Models.Pagination;
using Course.Catalog.Service.Api.Models.Paging;
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
    public async Task<Response<PagedList<CourseWithCategoryDto>>> GetAllWithCategoryAsync(PageRequest request, CancellationToken cancellationToken)
    {
        var courses = _mapper.Map<List<CourseWithCategoryDto>>(await _collection.Find(course => true).ToListAsync(cancellationToken));

        if (courses is null) return Response<PagedList<CourseWithCategoryDto>>.Fail("Course Not Found", 404);

        courses = courses.Select(course =>
        {
            var category = _mapper.Map<Response<CategoryDto>>(_categoryService.GetByIdAsync(course.CategoryId, cancellationToken).Result);
            course.Category = category.Data ?? null;
            return course;
        }).ToList();


        var pagedResponse = new PagedList<CourseWithCategoryDto>(
            courses,
            request.PageNumber,
            request.PageSize);

        return Response<PagedList<CourseWithCategoryDto>>.Success(pagedResponse, 200);
    }

    public async Task<Response<CourseWithCategoryDto>> GetByIdWithCategory(string id, CancellationToken cancellationToken)
    {
        var courses = await _collection.Find(x => true).ToListAsync(cancellationToken);
        var course = courses.FirstOrDefault(x => x.Id == id);

        if (course is null) return Response<CourseWithCategoryDto>.Fail($"No course has been found with {id}", 404);

        course.Category = _mapper.Map<Models.Category>(_categoryService.GetByIdAsync(course.CategoryId, cancellationToken).Result.Data);

        return Response<CourseWithCategoryDto>.Success(_mapper.Map<CourseWithCategoryDto>(course), 200);
    }

    public async Task<Response<PagedList<CourseWithCategoryDto>>> GetAllByUserIdWithCategory(string userId, PageRequest request, CancellationToken cancellationToken)
    {
        var courses = await _collection.Find(x => x.UserId == userId.ToString()).ToListAsync(cancellationToken: cancellationToken);

        var tasks = courses.Select(async course =>
        {
            var categoryResponse = await _categoryService.GetByIdAsync(course.CategoryId, cancellationToken);
            course.Category = _mapper.Map<Models.Category>(categoryResponse.Data);
            return course;
        }).ToList();

        var completedResult = await Task.WhenAll(tasks);
        var response = completedResult.ToList();

        var pagedResponse = new PagedList<CourseWithCategoryDto>(
            _mapper.Map<List<CourseWithCategoryDto>>(response),
            request.PageNumber, 
            request.PageSize);

        return Response<PagedList<CourseWithCategoryDto>>
            .Success(pagedResponse, 200);
    }
}