using Course.Invoice.Application.Abstractions.Messaging;

namespace Course.Invoice.Application.Invoice.Queries.GetInvoiceFileByOrderIdAndBuyerIdQuery;
public sealed record GetInvoiceFileByOrderIdAndBuyerIdQuery(int OrderId, string BuyerId) : IQuery<GetInvoiceFileByOrderIdAndBuyerIdResponse>;
