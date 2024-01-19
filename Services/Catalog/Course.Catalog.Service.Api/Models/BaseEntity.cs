using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Catalog.Service.Api.Models;

public abstract class BaseEntity
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedDate { get; set; }
}