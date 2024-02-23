using Course.Order.Application.Features.Orders.Constants;
using FluentValidation;

namespace Course.Order.Application.Features.Orders.Commands.CreateOrder;
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x=>x.Address)
            .NotEmpty()
            .WithMessage(OrderConstants.AddressCanNotBeEmpty);

        RuleFor(x => x.BuyerId)
            .NotEmpty()
            .WithMessage(OrderConstants.BuyerIdCanNotBeNull);

        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .WithMessage(OrderConstants.OrderItemsCanNotBeNull);
    }
}