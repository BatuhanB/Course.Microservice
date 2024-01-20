using AutoMapper;
using Course.Catalog.Service.Api.Dtos;
using Course.Catalog.Service.Api.Models;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Course.Catalog.Service.Api.Services.Generic;

public class GenericService<TEntity,TDto> : IGenericService<TEntity,TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    protected readonly IMongoCollection<TEntity> _collection;
    protected readonly IMapper _mapper;

    public GenericService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        var collectionName = databaseSettings.GetCollectionName<TEntity>();
        _collection = database.GetCollection<TEntity>(collectionName);
        _mapper = mapper;
    }
    
    public async Task<Response<List<TEntity>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = 
            await _collection.Find(x => true).ToListAsync(cancellationToken);
        return Response<List<TEntity>>.Success(entities, 200);
    }

    public async Task<Response<TEntity>> CreateAsync(TDto dto, CancellationToken cancellationToken)
    {
        dto.CreatedDate = DateTime.Now;
        var entity = _mapper.Map<TEntity>(dto);
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return Response<TEntity>.Success(entity,201);
    }

    public async Task<Response<NoContent>> UpdateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var updateEntity = _mapper.Map<TEntity>(dto);
        var result = await _collection.FindOneAndReplaceAsync(x => x.Id == dto.Id, updateEntity, cancellationToken: cancellationToken);
        return result is null ? 
            Response<NoContent>.Fail($"{typeof(TEntity)} has not found!", 404) :
            Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id,cancellationToken:cancellationToken);
        return result.DeletedCount > 0 ? 
            Response<NoContent>.Success(204) :
            Response<NoContent>.Fail($"{typeof(TEntity)} has not found!", 404);
    }

    public async Task<Response<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _collection.Find(x=>true).ToListAsync(cancellationToken);
        var result = entity.FirstOrDefault(x => x.Id == id);
        return result is null ? 
            Response<TEntity>.Fail($"No entity has found by {id}", 404) : 
            Response<TEntity>.Success(result,201);
    }
}