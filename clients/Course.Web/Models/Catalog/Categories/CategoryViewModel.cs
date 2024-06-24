namespace Course.Web.Models.Catalog.Categories;

public class CategoryViewModel
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public required string Name { get; set; }
}
