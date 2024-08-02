using Course.FakePayment.Service.Api.Models;

namespace Course.FakePayment.Service.Api.CommandSender;

public interface IOrderMessageCommandSender
{
    Task SendCommand(PaymentDto payment);
}
