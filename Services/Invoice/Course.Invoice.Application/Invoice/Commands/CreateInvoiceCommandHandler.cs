using Course.Invoice.Application.Abstractions.Data;
using Course.Invoice.Application.Abstractions.Messaging;
using Course.Invoice.Application.Abstractions.Services;
using Course.Invoice.Application.Invoice.Adapters;
using Course.Invoice.Domain.Invoice;
using Course.Shared.Dtos;

namespace Course.Invoice.Application.Invoice.Commands;
public class CreateInvoiceCommandHandler(
    IApplicationDbContext dbContext,
    IPdfConverterService pdfConverterService,
    IFileService fileService)
    : ICommandHandler<CreateInvoiceCommand, string>
{
    public async Task<Response<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<string>();
        var invoice =
            new Domain.Invoice.Invoice(
                new Customer().Map(request.Customer),
                new OrderInformation().Map(request.OrderInformation));

        await dbContext.Invoices.AddAsync(invoice, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        response.IsSuccessful = true;
        if (response.IsSuccessful)
        {
            var pdfBytes = pdfConverterService.GenerateInvoicePdf(invoice)
                ?? throw new ArgumentNullException("Pdf Could not Created");

            var fileUrl = await fileService.SaveInvoicePdf(invoice, pdfBytes)
                ?? throw new ArgumentNullException("File Could not Created");

            response.Data = fileUrl;
        }
        return response;
    }
}