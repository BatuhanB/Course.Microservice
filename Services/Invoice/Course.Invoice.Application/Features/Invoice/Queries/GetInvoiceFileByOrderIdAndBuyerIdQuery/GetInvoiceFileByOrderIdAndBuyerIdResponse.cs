namespace Course.Invoice.Application.Features.Invoice.Queries.GetInvoiceFileByOrderIdAndBuyerIdQuery;
public sealed record GetInvoiceFileByOrderIdAndBuyerIdResponse
{
    public string FileUrl { get; set; }
    public DateTime InvoiceCreatedDate { get; set; }
}