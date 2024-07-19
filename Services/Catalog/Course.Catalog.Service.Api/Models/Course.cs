using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Catalog.Service.Api.Models;

public class Course : BaseEntity
{
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    
    public string? Image { get; set; }
    
    public string? UserId { get; set; }

    public Feature? Feature { get; set; }
    public string CategoryId{ get; set; }
    
    [BsonIgnore]
    public Category? Category{ get; set; }
}