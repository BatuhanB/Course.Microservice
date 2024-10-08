﻿using Course.Invoice.Application.Abstractions.Messaging;

namespace Course.Invoice.Application.Features.Invoice.Commands.CreateInvoiceUrl;
public sealed record CreateInvoiceUrlCommand(string InvoiceId, int OrderId, string BuyerId, string FileUrl, DateTime InvoiceCreatedDate) : ICommand<bool>;