using AutoMapper;
using Course.Catalog.Service.Api.Dtos;
using Course.Catalog.Service.Api.Models;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Course.Catalog.Service.Api.Services.Generic;

public class GenericService<TResult, TEntity> : IGenericService<TResult, TEntity>
    where TResult : BaseDto
    where TEntity : BaseEntity
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

    public async Task<Response<List<TResult>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities =
            await _collection.Find(x => true).ToListAsync(cancellationToken);
        return Response<List<TResult>>.Success(_mapper.Map<List<TResult>>(entities), 200);
    }

    public async Task<Response<TResult>> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.CreatedDate = DateTime.Now;
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return Response<TResult>.Success(_mapper.Map<TResult>(entity), 201);
    }

    public async Task<Response<NoContent>> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var result = await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
        return result is null ?
            Response<NoContent>.Fail($"{typeof(TEntity)} has not found!", 404) :
            Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken: cancellationToken);
        return result.DeletedCount > 0 ?
            Response<NoContent>.Success(204) :
            Response<NoContent>.Fail($"{typeof(TEntity)} has not found!", 404);
    }

    public async Task<Response<TResult>> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var entity = await _collection.Find(x => true).ToListAsync(cancellationToken);
        var result = entity.FirstOrDefault(x => x.Id == id);
        return result is null ?
            Response<TResult>.Fail($"No entity has found by {id}", 404) :
            Response<TResult>.Success(_mapper.Map<TResult>(result), 201);
    }
}