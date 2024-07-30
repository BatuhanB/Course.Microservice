using Course.FakePayment.Service.Api.Models;
using Course.Shared.BaseController;
using Course.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Course.FakePayment.Service.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FakePaymentController : BaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment([FromBody] PaymentDto payment)
        {
            return CreateActionResultInstance(Response<PaymentDto>.Success(200));
        }
    }
}
