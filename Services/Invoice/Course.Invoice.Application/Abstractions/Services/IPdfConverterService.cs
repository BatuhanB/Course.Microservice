namespace Course.Invoice.Application.Abstractions.Services;
public interface IPdfConverterService
{
    byte[] GenerateInvoicePdf(Domain.Invoice.Invoice invoice);
}
