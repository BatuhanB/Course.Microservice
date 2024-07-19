using Course.Web.Models.Catalog.Categories;
using Course.Web.Models.Catalog.Features;

namespace Course.Web.Models.Catalog.Courses;

public class CourseUpdateInput
{
    public string Id { get; set; }
    public required string Name { get; set; }

    public required string Description { get; set; }

    public decimal Price { get; set; }

    public string? Image { get; set; }

    public string? UserId { get; set; }

    public FeatureViewModel? Feature { get; set; }
    public string CategoryId { get; set; }
}
