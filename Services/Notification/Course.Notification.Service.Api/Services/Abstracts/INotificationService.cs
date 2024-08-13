using Course.Notification.Service.Api.Models;
using Course.Shared.Dtos;

namespace Course.Notification.Service.Api.Services.Abstracts;

public interface INotificationService
{
    Task<Response<bool>> Delete(string id);
    Task<Response<Models.NotificationDto>> Get(string userId, string notificationId);
    Task<Response<List<Models.NotificationDto>>> GetAll(string userId,int count = 20);
    Task<Response<bool>> Save(Models.Notification notification);
    Task<Response<bool>> MarkAllAsRead(string userId);
}
