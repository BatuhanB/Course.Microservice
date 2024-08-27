using Course.Invoice.Application.Invoice.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Course.Invoice.Presentation.Controllers;
public class InvoiceController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
    {
        var response = await Sender.Send(command);
        return Ok(response);
    }
}
