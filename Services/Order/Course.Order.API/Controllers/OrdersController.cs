using Course.Order.Application.Contracts.Request;
using Course.Order.Application.Features.Orders.Commands.CreateOrder;
using Course.Order.Application.Features.Orders.Queries.GetOrdersByUserId;
using Course.Shared.BaseController;
using Course.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.Order.API.Controllers;

public class OrdersController(ISharedIdentityService identityService) : BaseController
{
    private readonly ISharedIdentityService _identityService = identityService;
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PageRequest request, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetOrdersByUserIdQuery(_identityService.GetUserId, request), cancellationToken);

        return CreateActionResultInstance(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(command, cancellationToken);

        return CreateActionResultInstance(result);
    }
}