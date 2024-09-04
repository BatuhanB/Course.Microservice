using Course.Invoice.Application.Invoice.Commands.Create;
using Course.Invoice.Application.Invoice.Queries.GetInvoiceFileByOrderIdAndBuyerIdQuery;
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

    [HttpGet]
    public async Task<IActionResult> GetFileUrl(GetInvoiceFileByOrderIdAndBuyerIdQuery query)
    {
        var response = await Sender.Send(query);
        return Ok(response);
    }
}