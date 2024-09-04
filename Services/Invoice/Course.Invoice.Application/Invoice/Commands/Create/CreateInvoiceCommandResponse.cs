namespace Course.Invoice.Application.Invoice.Commands.Create;
public sealed record CreateInvoiceCommandResponse
{
    public string InvoiceId { get; set; }
}