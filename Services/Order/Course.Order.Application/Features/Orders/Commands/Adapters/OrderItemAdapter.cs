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
            ProductId = orderItemForInvoice.ProductId,
            ProductName = orderItemForInvoice.ProductName,
            ImageUrl = orderItemForInvoice.ImageUrl,
            Price = orderItemForInvoice.Price,
            ProductOwnerId = orderItemForInvoice.ProductOwnerId
        };
    }
}