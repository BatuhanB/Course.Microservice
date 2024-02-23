using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.Order.API.Controllers.Base
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
