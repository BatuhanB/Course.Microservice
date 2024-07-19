namespace Course.Web.Models.Catalog.Courses;

public sealed record CourseWithCategoryViewModel
{
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public string? UserId { get; set; }
    public Features.FeatureViewModel? Feature { get; set; }
    public string CategoryId { get; set; }
    public Categories.CategoryViewModel? Category { get; set; }
}
