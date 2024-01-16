namespace Course.Catalog.Service.Api.Dtos;

public class CourseDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal Image { get; set; }
    
    public string? UserId { get; set; }
    
    public DateTime CreatedDate { get; set; }

    public FeatureDto Feature { get; set; }
    
    public required Guid CategoryId{ get; set; }
    
    public required CategoryDto Category{ get; set; }
}