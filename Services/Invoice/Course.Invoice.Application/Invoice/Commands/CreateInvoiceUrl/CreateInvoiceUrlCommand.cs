using Course.Invoice.Application.Abstractions.Messaging;

namespace Course.Invoice.Application.Invoice.Commands.CreateInvoiceUrl;
public sealed record CreateInvoiceUrlCommand(string InvoiceId,int OrderId,string BuyerId,string FileUrl, DateTime InvoiceCreatedDate) : ICommand<bool>;