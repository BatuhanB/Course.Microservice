namespace Course.Invoice.Application.Features.Invoice.Dtos.InvoiceCreate;
public class OrderItemDto
{
    public string ProductId { get; set; }
    public string ProductOwnerId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public string OrderInformationId { get; set; }
}