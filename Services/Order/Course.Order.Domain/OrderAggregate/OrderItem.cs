using Course.Order.Core;

namespace Course.Order.Domain.OrderAggregate;
public class OrderItem : Entity
{
    public string ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string ImageUrl { get; private set; }
    public Decimal Price { get; private set; }

    public OrderItem()
    {
    }

    public OrderItem(string productId, string productName, string imageUrl, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        ImageUrl = imageUrl;
        Price = price;
    }

    public void UpdateOrderItem(string productName, string imageUrl, decimal price)
    {
        ProductName = productName;
        Price = price;
        ImageUrl = imageUrl;
    }
}
