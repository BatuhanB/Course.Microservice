using AutoMapper;
using Course.Catalog.Service.Api.Dtos;
using Course.Catalog.Service.Api.Services.Category;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Catalog.Service.Api.Services.Course;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<Models.Course> _courseCollection;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CourseService(IMapper mapper,IDatabaseSettings databaseSettings,
        ICategoryService categoryService)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _courseCollection = database.GetCollection<Models.Course>(databaseSettings.CourseCollectionName);
        _mapper = mapper;
        _categoryService = categoryService;
    }

    public async Task<Response<List<CourseDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var courses = await _courseCollection.Find(course => true).ToListAsync(cancellationToken);

        if (courses is null) return Response<List<CourseDto>>.Fail("Course Not Found", 404);
        
        courses = courses.SelectMany(course =>
        {
            var category =
                _mapper.Map<Models.Category>(
                    _categoryService.GetById(course.CategoryId, cancellationToken).Result);
            course.Category = category;
            return courses;
        }).ToList();
        
        var result = _mapper.Map<List<CourseDto>>(courses);
        
        return Response<List<CourseDto>>.Success(result, 200);
    }
}