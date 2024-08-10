using Course.Notification.Service.Api.Hubs;
using Course.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Course.Notification.Service.Api.Consumers;

public class OrderCreatedNotificationEventConsumer(
    ILogger<OrderCreatedNotificationEventConsumer> logger, 
    IHubContext<NotificationHub> hubContext)
    : IConsumer<OrderCreatedNotificationEvent>
{
    private readonly ILogger<OrderCreatedNotificationEventConsumer> _logger = logger;
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<OrderCreatedNotificationEvent> context)
    {
        await _hubContext.Clients.User(context.Message.CourseOwnerId)
                .SendAsync("ReceiveMessage", $"Your course {context.Message.CourseName} was purchased on {context.Message.CourseBuyDate}");

        _logger.LogInformation($"{context.Message.CourseName} - {context.Message.CourseOwnerId} - {context.Message.CourseBuyDate}");
        await Task.CompletedTask;
    }
}
