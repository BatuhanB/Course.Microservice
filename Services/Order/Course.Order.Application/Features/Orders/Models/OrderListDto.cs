using Course.Order.Application.Contracts.Paging;
using Course.Order.Application.Dtos;

namespace Course.Order.Application.Features.Orders.Models;
public class OrderListDto : BasePageableModel
{
    public IList<OrderDto>? Items  { get; set; }
}