using AutoMapper;
using Course.Catalog.Service.Api.Dtos;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Catalog.Service.Api.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Models.Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Models.Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categories = _mapper.Map<List<CategoryDto>>(
            await _categoryCollection.FindAsync(category => true, cancellationToken: cancellationToken));

        return Response<List<CategoryDto>>.Success(categories, 200);
    } 
    
    public async Task<Response<CategoryDto>> CreateAsync(Models.Category category,CancellationToken cancellationToken)
    {
        await _categoryCollection.InsertOneAsync(category, cancellationToken: cancellationToken);
        var result = _mapper.Map<CategoryDto>(category);
        return Response<CategoryDto>.Success(result,201);
    } 
    
    public async Task<Response<CategoryDto>> GetById(Guid id,CancellationToken cancellationToken)
    {
        var category = await _categoryCollection.
            Find(x => x.Id == id).
            FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            return Response<CategoryDto>.Fail("Category Not Found", 404);
        }

        var result = _mapper.Map<CategoryDto>(category);
        return Response<CategoryDto>.Success(result, 200);
    }
}