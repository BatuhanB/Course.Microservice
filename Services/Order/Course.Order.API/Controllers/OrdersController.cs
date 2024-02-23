using Course.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;

namespace Course.Order.API.Controllers;
public class OrdersController : BaseController
{
    [HttpGet]
    public string Get()
    {
        return "a";
    }
}