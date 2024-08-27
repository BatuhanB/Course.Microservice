using Course.Invoice.Application.Abstractions.Data;
using Course.Invoice.Application.Abstractions.Messaging;
using Course.Invoice.Application.Invoice.Adapters;
using Course.Invoice.Domain.Invoice;
using Course.Shared.Dtos;

namespace Course.Invoice.Application.Invoice.Commands;
public class CreateInvoiceCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateInvoiceCommand, string>
{
    public async Task<Response<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var response = new Response<string>();
        var invoice =
            new Domain.Invoice.Invoice(
                DateTime.Now,
                new Customer().Map(request.Customer),
                new OrderInformation().Map(request.OrderInformation));

        await dbContext.Invoices.AddAsync(invoice, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        response.Data = invoice.Id;
        response.IsSuccessful = true;
        return response;
    }
}
