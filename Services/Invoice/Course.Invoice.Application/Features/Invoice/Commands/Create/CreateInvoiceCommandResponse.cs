namespace Course.Invoice.Application.Features.Invoice.Commands.Create;
public sealed record CreateInvoiceCommandResponse
{
    public string InvoiceId { get; set; }
}