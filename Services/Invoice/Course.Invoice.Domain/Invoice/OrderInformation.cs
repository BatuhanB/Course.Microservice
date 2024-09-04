using Course.Invoice.Domain.Core;

namespace Course.Invoice.Domain.Invoice;
public class OrderInformation : Entity
{
    public int OrderId { get; private set; }
    public string BuyerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);

    public OrderInformation()
    {
        
    }

    public OrderInformation(int orderId,string buyerId, DateTime orderDate)
    {
        OrderId = orderId;
        BuyerId = buyerId;
        OrderDate = orderDate;
        _orderItems = new ();
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        _orderItems.Add(orderItem);
    }
}