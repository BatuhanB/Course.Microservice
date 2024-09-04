namespace Course.Invoice.Application.Abstractions.Services;
public interface IFileService
{
    Task<(string FileUrlWithEnv, string FileUrlWithOutEnv)> SaveInvoicePdf(Domain.Invoice.Invoice invoice, byte[] pdfBytes);
}
