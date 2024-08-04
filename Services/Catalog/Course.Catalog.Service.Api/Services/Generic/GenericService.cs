using AutoMapper;
using Course.Catalog.Service.Api.Dtos;
using Course.Catalog.Service.Api.Models;
using Course.Catalog.Service.Api.Settings;
using Course.Shared.Dtos;
using MassTransit;
using MongoDB.Driver;
using Res = Course.Shared.Dtos;

namespace Course.Catalog.Service.Api.Services.Generic;

public class GenericService<TResult, TEntity> : IGenericService<TResult, TEntity>
    where TResult : BaseDto
    where TEntity : BaseEntity
{
    protected readonly IMongoCollection<TEntity> _collection;
    protected readonly IMapper _mapper;

    public GenericService(
        IMapper mapper, 
        IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        var collectionName = databaseSettings.GetCollectionName<TEntity>();
        _collection = database.GetCollection<TEntity>(collectionName);
        _mapper = mapper;
    }

    public async Task<Res.Response<List<TResult>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities =
            await _collection.Find(x => true).ToListAsync(cancellationToken);
        return Res.Response<List<TResult>>.Success(_mapper.Map<List<TResult>>(entities), 200);
    }

    public async Task<Res.Response<TResult>> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.CreatedDate = DateTime.Now;
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return Res.Response<TResult>.Success(_mapper.Map<TResult>(entity), 201);
    }

    public async Task<Res.Response<NoContent>> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.CreatedDate = DateTime.Now;
        var result = await _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
        return result is null ?
            Res.Response<NoContent>.Fail($"{typeof(TEntity)} has not found!", 404) :
            Res.Response<NoContent>.Success(204);
    }

    public async Task<Res.Response<NoContent>> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken: cancellationToken);
        return result.DeletedCount > 0 ?
            Res.Response<NoContent>.Success(204) :
            Res.Response<NoContent>.Fail($"{typeof(TEntity)} has not found!", 404);
    }

    public async Task<Res.Response<TResult>> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var entity = await _collection.Find(x => true).ToListAsync(cancellationToken);
        var result = entity.FirstOrDefault(x => x.Id == id);
        return result is null ?
            Res.Response<TResult>.Fail($"No entity has found by {id}", 404) :
            Res.Response<TResult>.Success(_mapper.Map<TResult>(result), 201);
    }
}