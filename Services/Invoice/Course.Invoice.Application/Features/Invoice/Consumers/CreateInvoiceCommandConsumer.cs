using Course.Invoice.Application.Abstractions.Services;
using Course.Invoice.Application.Features.Invoice.Adapters;
using Course.Invoice.Application.Features.Invoice.Dtos.InvoiceCreate;
using Course.Shared.Messages;
using MassTransit;
using MediatR;

namespace Course.Invoice.Application.Features.Invoice.Consumers;
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
            OrderId = context.Message.OrderId,
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
            new Commands.Create.CreateInvoiceCommand(
                customerDto,
                orderInformationDto
               );

        var createInvoiceResponse = await _mediator.Send(createInvoiceCommand);

        if (!createInvoiceResponse.IsSuccessful)
        {
            throw new ArgumentNullException("Invoice Could not Created");
        }

        var invoice = new Domain.Invoice.Invoice(
               new Domain.Invoice.Customer().Map(customerDto),
               new Domain.Invoice.OrderInformation().Map(orderInformationDto));

        var pdfBytes = _pdfConverterService.GenerateInvoicePdf(invoice)
            ?? throw new ArgumentNullException("Pdf Could not Created");

        var fileUrlResponse = await _fileService.SaveInvoicePdf(invoice, pdfBytes);

        if (string.IsNullOrWhiteSpace(fileUrlResponse.FileUrlWithOutEnv))
        {
            throw new ArgumentNullException("File Could not Saved");
        }


        var createInvoiceUrlCommand =
            new Commands.CreateInvoiceUrl.CreateInvoiceUrlCommand(
                createInvoiceResponse.Data!.InvoiceId,
                orderInformationDto.OrderId,
                orderInformationDto.BuyerId,
                fileUrlResponse.FileUrlWithOutEnv,
                orderInformationDto.OrderDate);

        var createInvoiceFileUrlResponse = await _mediator.Send(createInvoiceUrlCommand);

        if (!createInvoiceFileUrlResponse.IsSuccessful)
        {
            throw new Exception("Invoice FileUrl Could not Created");
        }
    }
}
