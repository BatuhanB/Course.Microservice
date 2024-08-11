using Course.Notification.Service.Api.Models;
using Course.Shared.Dtos;

namespace Course.Notification.Service.Api.Services.Abstracts;

public interface INotificationService
{
    Task<Response<bool>> Delete(string id);
    Task<Response<Models.NotificationDto>> Get(string userId, string notificationId);
    Task<Response<List<Models.NotificationDto>>> GetAll(string userId);
    Task<Response<bool>> SaveOrUpdate(Models.Notification notification);
    Task<Response<bool>> MarkAllAsRead(string userId);
}
