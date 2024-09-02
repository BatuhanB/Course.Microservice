namespace Course.Invoice.Application.Abstractions.Services;
public interface IFileService
{
    Task<string> SaveInvoicePdf(Domain.Invoice.Invoice invoice, byte[] pdfBytes, string scheme, string host);
}
