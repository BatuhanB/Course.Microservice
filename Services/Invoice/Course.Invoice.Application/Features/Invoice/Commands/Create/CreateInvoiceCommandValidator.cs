using Course.Invoice.Application.Features.Invoice.Constants;
using FluentValidation;

namespace Course.Invoice.Application.Features.Invoice.Commands.Create;
public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x => x.Customer)
            .NotNull()
            .WithMessage(Messages.CUSTOMER_CAN_NOT_NULL);

        RuleFor(x => x.OrderInformation)
            .NotNull()
            .WithMessage(Messages.ORDER_INFORMATION_CAN_NOT_NULL);
    }
}
