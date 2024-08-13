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
        var notification = new Models.Notification(context.Message.CourseName, notificationBody, context.Message.CourseOwnerId);
        var res = await _notificationService.Save(notification);

        if (res.IsSuccessful)
        {
            await SendNotificationsToClient(notification);
        }
        else
        {
            _logger.LogError($"Error occured while saving notification!");
        }
    }

    private async Task SendNotificationsToClient(Models.Notification notification)
    {
        var newNotification = await _notificationService.Get(notification.UserId, notification.Id);
        await _hubContext.Clients.User(notification.UserId)
            .SendAsync("ReceiveMessage", newNotification.Data);

        _logger.LogInformation($"{notification.Title} - {notification.UserId} - {notification.CreatedDate}");
    }
}