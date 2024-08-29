using Course.Shared.Messages;

namespace Course.Order.Application.Features.Orders.Commands.Adapters;
public static class OrderItemAdapter
{
    public static OrderItemForInvoice Map(
        this OrderItemForInvoice orderItemForInvoice, 
        Domain.OrderAggregate.OrderItem orderItem)
    {
        return new OrderItemForInvoice
        {
            ProductId = orderItem.ProductId,
            ProductName = orderItem.ProductName,
            ImageUrl = orderItem.ImageUrl,
            Price = orderItem.Price,
            ProductOwnerId = orderItem.ProductOwnerId,
        };
    }
}