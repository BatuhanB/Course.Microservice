using Course.Invoice.Application.Invoice.Dtos.InvoiceCreate;
using Course.Shared.Messages;
using MassTransit;
using MediatR;

namespace Course.Invoice.Application.Invoice.Consumers;
public class CreateInvoiceCommandConsumer : IConsumer<CreateInvoiceCommand>
{
    private readonly IMediator _mediator;

    public CreateInvoiceCommandConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateInvoiceCommand> context)
    {
        var createInvoiceCommand =
            new Commands.CreateInvoiceCommand(
                new CustomerDto()
                {
                    UserName = context.Message.Customer.UserName,
                    Address = new AddressDto
                    {
                        District = context.Message.Customer.Address.District,
                        Province = context.Message.Customer.Address.Province,
                        Street = context.Message.Customer.Address.Street,
                        ZipCode = context.Message.Customer.Address.ZipCode,
                        Line = context.Message.Customer.Address.Line
                    },
                },
                new OrderInformationDto
                {
                    BuyerId = context.Message.OrderInformation.BuyerId,
                    OrderDate = context.Message.OrderInformation.OrderDate,
                    OrderItems = context.Message.OrderInformation.OrderItems
                    .Select(item =>
                    {
                        return new OrderItemDto
                        {
                            ImageUrl = item.ImageUrl,
                            OrderInformationId = item.OrderInformationId,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            ProductName = item.ProductName,
                            ProductOwnerId = item.ProductOwnerId
                        };
                    }).ToList()
                });

        await _mediator.Send(createInvoiceCommand);
    }
}
