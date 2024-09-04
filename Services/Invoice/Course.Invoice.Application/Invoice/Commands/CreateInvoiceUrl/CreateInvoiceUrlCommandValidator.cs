using Course.Invoice.Application.Invoice.Constants;
using FluentValidation;

namespace Course.Invoice.Application.Invoice.Commands.CreateInvoiceUrl;
public class CreateInvoiceUrlCommandValidator
    : AbstractValidator<CreateInvoiceUrlCommand>
{
    public CreateInvoiceUrlCommandValidator()
    {
        RuleFor(x=>x.OrderId)
            .NotEmpty()
            .WithMessage(Messages.ORDER_ID_CAN_NOT_BE_NULL)
            .GreaterThan(0)
            .WithMessage(Messages.ORDER_ID_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(x=>x.BuyerId)
            .NotEmpty()
            .WithMessage(Messages.BUYER_ID_CAN_NOT_BE_NULL);

        RuleFor(x => x.InvoiceId)
            .NotEmpty()
            .WithMessage(Messages.INVOICE_ID_CAN_NOT_BE_NULL);

        RuleFor(x => x.FileUrl)
            .NotEmpty()
            .WithMessage(Messages.INVOICE_FILEURL_CAN_NOT_BE_NULL);

        RuleFor(x => x.InvoiceCreatedDate)
            .NotEmpty()
            .WithMessage(Messages.INVOICE_CREATED_DATE_CAN_NOT_BE_NULL);
    }
}
