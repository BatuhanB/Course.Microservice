namespace Course.Catalog.Service.Api.Dtos;

public class CourseCreateDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Image { get; set; }
    
    public string? UserId { get; set; }

    public FeatureDto? Feature { get; set; }
    
    public required Guid CategoryId{ get; set; }
}