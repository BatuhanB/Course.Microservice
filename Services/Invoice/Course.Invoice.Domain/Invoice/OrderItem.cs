
using Course.Invoice.Domain.Core;

namespace Course.Invoice.Domain.Invoice;
public class OrderItem : Entity
{
    public string ProductId { get; private set; }
    public string ProductOwnerId { get; private set; }
    public string ProductName { get; private set; }
    public string ImageUrl { get; private set; }
    public Decimal Price { get; private set; }
    public string OrderInformationId { get; private set; }

    public OrderItem()
    {
    }

    public OrderItem(string productId, string productName, string imageUrl, decimal price,string productOwnerId , string orderInformationId)
    {
        ProductOwnerId = productOwnerId;
        ProductId = productId;
        ProductName = productName;
        ImageUrl = imageUrl;
        Price = price;
        OrderInformationId = orderInformationId;
    }

    public void UpdateOrderItem(string productName, string imageUrl, decimal price)
    {
        ProductName = productName;
        Price = price;
        ImageUrl = imageUrl;
    }
}
