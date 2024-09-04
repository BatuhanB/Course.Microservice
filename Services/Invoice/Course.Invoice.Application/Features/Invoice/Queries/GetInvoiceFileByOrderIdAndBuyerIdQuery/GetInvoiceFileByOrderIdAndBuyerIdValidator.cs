using Course.Invoice.Application.Features.Invoice.Constants;
using FluentValidation;

namespace Course.Invoice.Application.Features.Invoice.Queries.GetInvoiceFileByOrderIdAndBuyerIdQuery;
public class GetInvoiceFileByOrderIdAndBuyerIdValidator
    : AbstractValidator<GetInvoiceFileByOrderIdAndBuyerIdQuery>
{
    public GetInvoiceFileByOrderIdAndBuyerIdValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage(Messages.ORDER_ID_CAN_NOT_BE_NULL)
            .GreaterThan(0)
            .WithMessage(Messages.ORDER_ID_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(x => x.BuyerId)
            .NotEmpty()
            .WithMessage(Messages.BUYER_ID_CAN_NOT_BE_NULL);
    }
}