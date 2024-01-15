using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Catalog.Service.Api.Models;

public class Category
{
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
}