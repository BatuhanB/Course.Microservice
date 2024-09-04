using Course.Invoice.Application.Abstractions.Data;
using Course.Invoice.Application.Abstractions.Messaging;
using Course.Invoice.Application.Features.Invoice.Constants;
using Course.Shared.Dtos;

namespace Course.Invoice.Application.Features.Invoice.Commands.CreateInvoiceUrl;
public class CreateInvoiceUrlCommandHandler(
    IApplicationDbContext dbContext)
    : ICommandHandler<CreateInvoiceUrlCommand, bool>
{
    public async Task<Response<bool>> Handle(CreateInvoiceUrlCommand request, CancellationToken cancellationToken)
    {
        var invoiceUrl = new Domain.Invoice.InvoiceFileUrl(
            request.InvoiceId,
            request.FileUrl,
            request.OrderId,
            request.BuyerId,
            request.InvoiceCreatedDate);

        await dbContext.InvoiceFileUrls.AddAsync(invoiceUrl);

        var res = await dbContext.SaveChangesAsync();
        return res > 0
            ? Response<bool>.Success(204)
            : Response<bool>.Fail(Messages.INVOICE_FILEURL_COULD_NOT_CREATED, 400);
    }
}
