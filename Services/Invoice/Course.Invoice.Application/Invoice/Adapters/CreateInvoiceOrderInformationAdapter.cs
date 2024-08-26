using Course.Invoice.Application.Invoice.Dtos.InvoiceCreate;
using Course.Invoice.Domain.Invoice;

namespace Course.Invoice.Application.Invoice.Adapters;
public static class CreateInvoiceOrderInformationAdapter
{
    public static OrderInformation Map(this OrderInformation orderInformation, OrderInformationDto orderInformationDto)
    {
        orderInformation =
            new OrderInformation(
                orderInformationDto.BuyerId,
                orderInformationDto.OrderDate);

        foreach (var orderItem in orderInformationDto.OrderItems)
        {
            orderInformation.AddOrderItem(new OrderItem().Map(orderItem));
        }
        return orderInformation;
    }
}
