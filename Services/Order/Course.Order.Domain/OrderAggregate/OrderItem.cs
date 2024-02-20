namespace Course.Order.Domain.OrderAggregate;
public class OrderItem(string productId, string productName, string imageUrl, decimal price)
{
    public string ProductId { get; private set; } = productId;
    public string ProductName { get; private set; } = productName;
    public string ImageUrl { get; private set; } = imageUrl;
    public decimal Price { get; private set; } = price;

    public void UpdateOrderItem(
        string productName, 
        string imageUrl,
        decimal price)
    {
        ProductName = productName;
        ImageUrl = imageUrl;
        Price = price;
    }
}
