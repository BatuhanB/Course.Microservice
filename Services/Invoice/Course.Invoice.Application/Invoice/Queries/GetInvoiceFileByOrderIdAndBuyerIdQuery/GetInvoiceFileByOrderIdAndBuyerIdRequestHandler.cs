using Course.Invoice.Application.Abstractions.Data;
using Course.Invoice.Application.Abstractions.Messaging;
using Course.Invoice.Application.Invoice.Constants;
using Course.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Course.Invoice.Application.Invoice.Queries.GetInvoiceFileByOrderIdAndBuyerIdQuery;

public class GetInvoiceFileByOrderIdAndBuyerIdRequestHandler(
    IApplicationDbContext dbContext)
    : IQueryHandler<GetInvoiceFileByOrderIdAndBuyerIdQuery, GetInvoiceFileByOrderIdAndBuyerIdResponse>
{
    public async Task<Response<GetInvoiceFileByOrderIdAndBuyerIdResponse>> Handle(GetInvoiceFileByOrderIdAndBuyerIdQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<GetInvoiceFileByOrderIdAndBuyerIdResponse>();
        response.IsSuccessful = true;
        response.StatusCode = 204;

        var invoiceFileUrl = await dbContext
            .InvoiceFileUrls
            .AsNoTracking()
            .Where(x => 
            x.OrderId == request.OrderId &&
            x.BuyerId == request.BuyerId)
            .FirstOrDefaultAsync();

        if (invoiceFileUrl == null)
        {
            response.IsSuccessful = false;
            response.Errors.Add(Messages.INVOICE_FILEURL_COULD_NOT_FOUND);
            response.StatusCode = 404;
            return response;
        }

        response.Data!.FileUrl = invoiceFileUrl.FileUrl;

        return response;
    }
}