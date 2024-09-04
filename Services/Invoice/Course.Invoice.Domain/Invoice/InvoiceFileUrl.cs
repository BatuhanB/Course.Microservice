using Course.Invoice.Domain.Core;

namespace Course.Invoice.Domain.Invoice;
public class InvoiceFileUrl : Entity
{
    public string InvoiceId { get; private set; }
    public string FileUrl { get; private set; }
    public int OrderId { get; private set; }
    public string BuyerId { get; private set; }
    public DateTime InvoiceCreatedDate { get; private set; }

    public InvoiceFileUrl() { }

    public InvoiceFileUrl(
        string invoiceId, 
        string fileUrl, 
        int orderId, 
        string buyerId, 
        DateTime invoiceCreatedDate)
    {
        InvoiceId = invoiceId;
        FileUrl = fileUrl;
        OrderId = orderId;
        BuyerId = buyerId;
        InvoiceCreatedDate = invoiceCreatedDate;
    }
}