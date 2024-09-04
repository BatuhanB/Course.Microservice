using Course.Invoice.Application.Abstractions.Messaging;
using Course.Invoice.Application.Invoice.Dtos.InvoiceCreate;

namespace Course.Invoice.Application.Invoice.Commands.Create;
public sealed record CreateInvoiceCommand(CustomerDto Customer, OrderInformationDto OrderInformation) : ICommand<CreateInvoiceCommandResponse>;
