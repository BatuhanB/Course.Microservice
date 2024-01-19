using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Catalog.Service.Api.Models;

public abstract class BaseEntity
{
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; } = Guid.NewGuid();
}