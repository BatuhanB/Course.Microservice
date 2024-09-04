using Course.Invoice.Application.Features.Invoice.Dtos.InvoiceCreate;
using Course.Invoice.Domain.Invoice;

namespace Course.Invoice.Application.Features.Invoice.Adapters;
public static class CreateInvoiceOrderItemAdapter
{
    public static OrderItem Map(this OrderItem orderItem, OrderItemDto orderItemDto)
    {
        orderItem =
            new OrderItem(
                orderItemDto.ProductId,
                orderItemDto.ProductName,
                orderItemDto.ImageUrl,
                orderItemDto.Price,
                orderItemDto.ProductOwnerId,
                orderItemDto.OrderInformationId);

        return orderItem;
    }
}
