using AutoMapper;
using Course.FakePayment.Service.Api.Models;
using Course.Shared.Messages;
using MassTransit;

namespace Course.FakePayment.Service.Api.CommandSender;

public class OrderMessageCommandSender
    (ISendEndpointProvider sendEndpointProvider, IMapper mapper) 
    : IOrderMessageCommandSender
{
    private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;
    private readonly IMapper _mapper = mapper;

    public async Task SendCommand(PaymentDto payment)
    {
        var command = _mapper.Map<CreateOrderMessageCommand>(payment);
        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-orderq"));
        await sendEndpoint.Send(command);
    }
}
