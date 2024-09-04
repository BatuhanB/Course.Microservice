using Course.Invoice.Application.Features.Invoice.Dtos.InvoiceCreate;
using Course.Invoice.Domain.Invoice;

namespace Course.Invoice.Application.Features.Invoice.Adapters;
public static class CreateInvoiceOrderInformationAdapter
{
    public static OrderInformation Map(this OrderInformation orderInformation, OrderInformationDto orderInformationDto)
    {
        orderInformation =
            new OrderInformation(
                orderInformation.OrderId,
                orderInformationDto.BuyerId,
                orderInformationDto.OrderDate);

        foreach (var orderItem in orderInformationDto.OrderItems)
        {
            orderInformation.AddOrderItem(new OrderItem().Map(orderItem));
        }
        return orderInformation;
    }
}
