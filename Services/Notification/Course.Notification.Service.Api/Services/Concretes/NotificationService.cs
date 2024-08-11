using Course.Notification.Service.Api.Services.Abstracts;
using Newtonsoft.Json;
using Sha = Course.Shared.Dtos;

namespace Course.Notification.Service.Api.Services.Concretes
{
    public class NotificationService(RedisService redisService) : INotificationService
    {
        private readonly RedisService _redisService = redisService;

        public async Task<Sha.Response<bool>> Delete(string userId)
        {
            var db = _redisService.GetDb();
            var result = await db.KeyDeleteAsync(userId);
            return result
                ? Sha.Response<bool>.Success(true, 200)
                : Sha.Response<bool>.Fail("Notifications not found", 404);
        }

        public async Task<Sha.Response<Models.NotificationDto>> Get(string userId, string notificationId)
        {
            var db = _redisService.GetDb();
            var notifications = await db.ListRangeAsync(userId);

            var notification = notifications
                .Select(n => JsonConvert.DeserializeObject<Models.NotificationDto>(n))
                .FirstOrDefault(n => n.Id == notificationId);

            return notification != null
                ? Sha.Response<Models.NotificationDto>.Success(notification, 200)
                : Sha.Response<Models.NotificationDto>.Fail("Notification not found", 404);
        }

        public async Task<Sha.Response<List<Models.NotificationDto>>> GetAll(string userId)
        {
            var db = _redisService.GetDb();
            var notifications = await db.ListRangeAsync(userId);

            var notificationList = notifications
                .Select(n => JsonConvert.DeserializeObject<Models.NotificationDto>(n))
                .ToList();

            return notificationList.Any()
                ? Sha.Response<List<Models.NotificationDto>>.Success(notificationList, 200)
                : Sha.Response<List<Models.NotificationDto>>.Fail("No notifications found", 404);
        }

        public async Task<Sha.Response<bool>> SaveOrUpdate(Models.Notification notification)
        {
            if (notification == null)
            {
                return Sha.Response<bool>.Fail("Notification cannot be null", 400);
            }

            var db = _redisService.GetDb();
            var serializedNotification = JsonConvert.SerializeObject(notification);

            // Add or update the notification in the list
            await db.ListRightPushAsync(notification.UserId, serializedNotification);

            return Sha.Response<bool>.Success(true, 200);
        }

        public async Task<Sha.Response<bool>> MarkAllAsRead(string userId)
        {
            var db = _redisService.GetDb();
            var notifications = await db.ListRangeAsync(userId);

            if (!notifications.Any())
            {
                return Sha.Response<bool>.Fail("No notifications found", 404);
            }

            var notificationList = notifications
                .Select(n => JsonConvert.DeserializeObject<Models.NotificationDto>(n))
                .ToList();

            var updatedNotifications = notificationList.Select(notification =>
            {
                var updatedNotification = new Models.Notification(notification!.Id, notification.Title, notification.Description, true, notification.UserId, notification.CreatedDate);

                return JsonConvert.SerializeObject(updatedNotification);
            });

            // Clear existing notifications and push updated ones
            await db.KeyDeleteAsync(userId);
            foreach (var updatedNotification in updatedNotifications)
            {
                await db.ListRightPushAsync(userId, updatedNotification);
            }

            return Sha.Response<bool>.Success(true, 200);
        }
    }
}
