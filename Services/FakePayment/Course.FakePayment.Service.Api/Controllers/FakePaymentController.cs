using Course.FakePayment.Service.Api.CommandSender;
using Course.FakePayment.Service.Api.Models;
using Course.Shared.BaseController;
using Course.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Course.FakePayment.Service.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public partial class FakePaymentController(IOrderMessageCommandSender messageSender) : BaseController
{
    private readonly IOrderMessageCommandSender _messageSender = messageSender;

    [HttpPost]
    public async Task<IActionResult> ReceivePayment([FromBody] PaymentDto payment)
    {
        var response = new PaymentResponse
        {
            Message = "Payment has successfully completed."
        };
        await _messageSender.SendCommand(payment);

        return CreateActionResultInstance(Response<PaymentResponse>.Success(200));
    }
}
