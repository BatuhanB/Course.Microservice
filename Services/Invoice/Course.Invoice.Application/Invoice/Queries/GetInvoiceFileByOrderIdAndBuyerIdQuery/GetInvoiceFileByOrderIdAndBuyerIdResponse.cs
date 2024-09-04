namespace Course.Invoice.Application.Invoice.Queries.GetInvoiceFileByOrderIdAndBuyerIdQuery;
public sealed record GetInvoiceFileByOrderIdAndBuyerIdResponse
{
    public string FileUrl { get; set; }
}