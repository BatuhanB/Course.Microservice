using Course.Order.Service.Api.Services.Contracts;
using Course.Shared.Messages;
using MassTransit;

namespace Course.Basket.Service.Api.Consumers;

public class BasketCourseNameUpdatedEventConsumer(
    IBasketService basketService,
    ILogger<BasketCourseNameUpdatedEventConsumer> logger)
    : IConsumer<BasketCourseNameUpdatedEvent>
{
    private readonly IBasketService _basketService = basketService;
    private readonly ILogger<BasketCourseNameUpdatedEventConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<BasketCourseNameUpdatedEvent> context)
    {
        var basket = await _basketService.Get(context.Message.UserId);
        if (basket == null)
        {
            _logger.LogError($"{nameof(basket)} is empty!");
        }
        else
        {
            foreach (var basketItem in basket!.Data!.BasketItems!)
            {
                if (basketItem.CourseId == context.Message.CourseId)
                {
                    basketItem.CourseName = context.Message.CourseName;
                }
            }
            await _basketService.SaveOrUpdate(basket.Data);
        }
    }
}
