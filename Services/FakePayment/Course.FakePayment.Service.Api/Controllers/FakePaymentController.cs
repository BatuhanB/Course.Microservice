using Course.Shared.BaseController;
using Course.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Course.FakePayment.Service.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FakePaymentController : BaseController
    {
        [HttpGet]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
