using Course.Invoice.Application.Abstractions.Messaging;
using Course.Invoice.Application.Features.Invoice.Dtos.InvoiceCreate;

namespace Course.Invoice.Application.Features.Invoice.Commands.Create;
public sealed record CreateInvoiceCommand(CustomerDto Customer, OrderInformationDto OrderInformation) : ICommand<CreateInvoiceCommandResponse>;
