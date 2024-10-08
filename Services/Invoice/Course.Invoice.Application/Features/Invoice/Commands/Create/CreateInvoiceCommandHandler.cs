﻿using Course.Invoice.Application.Abstractions.Data;
using Course.Invoice.Application.Abstractions.Messaging;
using Course.Invoice.Application.Features.Invoice.Adapters;
using Course.Invoice.Domain.Invoice;
using Course.Shared.Dtos;

namespace Course.Invoice.Application.Features.Invoice.Commands.Create;
public class CreateInvoiceCommandHandler(
    IApplicationDbContext dbContext)
    : ICommandHandler<CreateInvoiceCommand, CreateInvoiceCommandResponse>
{
    public async Task<Response<CreateInvoiceCommandResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        
        var invoice =
            new Domain.Invoice.Invoice(
                new Customer().Map(request.Customer),
                new OrderInformation().Map(request.OrderInformation));

        await dbContext.Invoices.AddAsync(invoice, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new Response<CreateInvoiceCommandResponse>()
        {
            Data = new CreateInvoiceCommandResponse() {InvoiceId = invoice.Id},
            IsSuccessful = true,
            StatusCode = 200
        };
       
        return response;
    }
}