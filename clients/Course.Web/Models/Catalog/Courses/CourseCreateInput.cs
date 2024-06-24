using Course.Web.Models.Catalog.Categories;
using Course.Web.Models.Catalog.Features;

namespace Course.Web.Models.Catalog.Courses;

public class CourseCreateInput
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public decimal Price { get; set; }

    public string? Image { get; set; }

    public string? UserId { get; set; }

    public FeatureViewModel? Feature { get; set; }
    public Guid CategoryId { get; set; }

    public CategoryViewModel? Category { get; set; }
}
