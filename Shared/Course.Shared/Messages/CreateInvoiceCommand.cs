namespace Course.Shared.Messages;
public class CreateInvoiceCommand
{
    public int OrderId { get; set; }
    public CustomerForInvoice Customer { get;  set; }
    public OrderInformationForInvoice OrderInformation { get;  set; }
}

public class CustomerForInvoice
{
    public string UserName { get;  set; }
    public AddressForInvoice Address { get;  set; }
}

public class AddressForInvoice
{
    public string Province { get; set; }
    public string District { get; set; }
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string Line { get; set; }
}

public class OrderInformationForInvoice
{
    public string BuyerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemForInvoice> OrderItems { get; set; }
}

public class OrderItemForInvoice
{
    public string ProductId { get; set; }
    public string ProductOwnerId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public Decimal Price { get; set; }
    public string OrderInformationId { get; set; }
}