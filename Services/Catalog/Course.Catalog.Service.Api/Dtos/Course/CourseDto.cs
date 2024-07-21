using Course.Catalog.Service.Api.Dtos.Feature;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Course.Catalog.Service.Api.Dtos.Course;

public class CourseDto : BaseDto
{
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    
    public string? Image { get; set; }
    
    public string? UserId { get; set; }
    
    public FeatureDto? Feature { get; set; }
    
    public string CategoryId{ get; set; }
}