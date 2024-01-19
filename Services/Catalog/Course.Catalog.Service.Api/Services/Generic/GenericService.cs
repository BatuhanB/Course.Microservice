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
        var entities = _mapper.Map<List<TEntity>>(
            await _collection.FindAsync(category => true, cancellationToken: cancellationToken));
        return Response<List<TEntity>>.Success(entities, 200);
    }

    public async Task<Response<TEntity>> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        var result = _mapper.Map<TEntity>(entity);
        return Response<TEntity>.Success(result,201);
    }

    public async Task<Response<TEntity>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<TEntity>.Filter.Eq(x=>x.Id, id);
        var entity = await _collection.Find(filter).FirstOrDefaultAsync();
        if(entity is null ) throw new ArgumentNullException($"Collection name not defined for entity type {entity}");
        return Response<TEntity>.Success(entity,201);
    }
}