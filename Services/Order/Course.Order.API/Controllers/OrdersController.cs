using Microsoft.AspNetCore.Mvc;

namespace Course.Order.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "a";
        }
    }
}
