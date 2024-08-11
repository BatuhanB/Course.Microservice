using Course.Notification.Service.Api.Hubs;
using Course.Notification.Service.Api.Services.Abstracts;
using Course.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace Course.Notification.Service.Api.Consumers;

public class OrderCreatedNotificationEventConsumer(
    ILogger<OrderCreatedNotificationEventConsumer> logger,
    IHubContext<NotificationHub> hubContext,
    INotificationService notificationService)
    : IConsumer<OrderCreatedNotificationEvent>
{
    private readonly ILogger<OrderCreatedNotificationEventConsumer> _logger = logger;
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;
    private readonly INotificationService _notificationService = notificationService;

    public async Task Consume(ConsumeContext<OrderCreatedNotificationEvent> context)
    {
        var notificationBody = $"Your course {context.Message.CourseName} was purchased on {context.Message.CourseBuyDate}";
        var notification = new Models.Notification(context.Message.CourseName, notificationBody, false, context.Message.CourseOwnerId);
        var res = await _notificationService.SaveOrUpdate(notification);

        if (res.IsSuccessful)
        {
            var notifications = await _notificationService.GetAll(context.Message.CourseOwnerId);
            await _hubContext.Clients.User(context.Message.CourseOwnerId)
                .SendAsync("ReceiveNotification", notifications);

            _logger.LogInformation($"{context.Message.CourseName} - {context.Message.CourseOwnerId} - {context.Message.CourseBuyDate}");
        }
        else
        {
            _logger.LogError($"Error occured while saving notification!");
        }
    }
}