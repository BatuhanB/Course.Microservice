namespace Course.Invoice.Application.Invoice.Dtos.InvoiceCreate;
public class OrderInformationDto
{
    public int OrderId { get; set; }
    public string BuyerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemDto> OrderItems {get;set; }
}
