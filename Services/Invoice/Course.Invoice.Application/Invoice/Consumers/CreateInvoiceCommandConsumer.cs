using Course.Invoice.Application.Abstractions.Services;
using Course.Invoice.Application.Invoice.Adapters;
using Course.Invoice.Application.Invoice.Dtos.InvoiceCreate;
using Course.Shared.Messages;
using MassTransit;
using MediatR;

namespace Course.Invoice.Application.Invoice.Consumers;
public class CreateInvoiceCommandConsumer : IConsumer<CreateInvoiceCommand>
{
    private readonly IMediator _mediator;
    private readonly IPdfConverterService _pdfConverterService;
    private readonly IFileService _fileService;

    public CreateInvoiceCommandConsumer(
        IMediator mediator, 
        IPdfConverterService pdfConverterService,
        IFileService fileService)
    {
        _mediator = mediator;
        _pdfConverterService = pdfConverterService;
        _fileService = fileService;
    }

    public async Task Consume(ConsumeContext<CreateInvoiceCommand> context)
    {
        var customerDto = new CustomerDto()
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
        };

        var orderInformationDto = new OrderInformationDto
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
        };

        var createInvoiceCommand =
            new Commands.CreateInvoiceCommand(
                customerDto,
                orderInformationDto
               );

        var response = await _mediator.Send(createInvoiceCommand);
    }
}
