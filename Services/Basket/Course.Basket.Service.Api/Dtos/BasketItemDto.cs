namespace Course.Order.Service.Api.Dtos;
public class BasketItemDto
{
    public required int Quantity { get; set; }
    public required string CourseId { get; set; }
    public required string CourseOwnerId { get; set; }
    public required string CourseName { get; set; }
    public required decimal Price { get; set; }
    public required decimal PriceWithDiscount { get; set; }
}