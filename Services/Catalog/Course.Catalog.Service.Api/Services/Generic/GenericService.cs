using AutoMapper;
using Course.Catalog.Service.Api.Models;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MongoDB.Driver;

namespace Course.Catalog.Service.Api.Services.Generic;

public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseEntity
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
            await _collection.Find(category => true).ToListAsync(cancellationToken);
        return Response<List<TEntity>>.Success(entities, 200);
    }

    public async Task<Response<TEntity>> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.CreatedDate = DateTime.Now;
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        var result = _mapper.Map<TEntity>(entity);
        return Response<TEntity>.Success(result,201);
    }

    public async Task<Response<NoContent>> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var updateEntity = _mapper.Map<TEntity>(entity);
        var result = await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, updateEntity, cancellationToken: cancellationToken);
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
        var filter = Builders<TEntity>.Filter.Eq(x=>x.Id, id);
        var entity = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        return entity is null ? 
            Response<TEntity>.Fail($"Collection name not defined for entity type {entity}", 404) : 
            Response<TEntity>.Success(entity,201);
    }
}