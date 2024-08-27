using Course.Notification.Service.Api.Services.Abstracts;
using Newtonsoft.Json;
using StackExchange.Redis;
using Sha = Course.Shared.Dtos;

namespace Course.Notification.Service.Api.Services.Concretes
{
    public class NotificationService(RedisService redisService) : INotificationService
    {
        private readonly RedisService _redisService = redisService;

        public async Task<Sha.Response<bool>> Delete(string userId)
        {
            var db = _redisService.GetDb();
            var result = await db.KeyDeleteAsync($"notifications:{userId}");
            return result
                ? Sha.Response<bool>.Success(true, 200)
                : Sha.Response<bool>.Fail("Notifications not found", 404);
        }

        public async Task<Sha.Response<Models.NotificationDto>> Get(string userId, string notificationId)
        {
            var db = _redisService.GetDb();
            var notifications = await db.SortedSetRangeByScoreAsync($"notifications:{userId}");

            var notification = notifications
                .Select(n => JsonConvert.DeserializeObject<Models.NotificationDto>(n))
                .FirstOrDefault(n => n.Id == notificationId);

            return notification != null
                ? Sha.Response<Models.NotificationDto>.Success(notification, 200)
                : Sha.Response<Models.NotificationDto>.Fail("Notification not found", 404);
        }

        public async Task<Sha.Response<List<Models.NotificationDto>>> GetAll(string userId,int count = 20)
        {
            var db = _redisService.GetDb();
            userId = $"notifications:{userId}";
            var notifications = await db.SortedSetRangeByScoreAsync(userId,
                order:Order.Descending,
                take:count);

            var notificationList = notifications
                .Select(n => JsonConvert.DeserializeObject<Models.NotificationDto>(n))
                .ToList();

            return notificationList.Any()
                ? Sha.Response<List<Models.NotificationDto>>.Success(notificationList, 200)
                : Sha.Response<List<Models.NotificationDto>>.Fail("No notifications found", 404);
        }

        public async Task<Sha.Response<bool>> Save(Models.Notification notification)
        {
            if (notification == null)
            {
                return Sha.Response<bool>.Fail("Notification cannot be null", 400);
            }

            var db = _redisService.GetDb();
            var serializedNotification = JsonConvert.SerializeObject(notification);

            // Use the notification timestamp as the score for the sorted set
            var score = new DateTimeOffset(notification.CreatedDate).ToUnixTimeSeconds();
            await db.SortedSetAddAsync($"notifications:{notification.UserId}", serializedNotification, score);

            return Sha.Response<bool>.Success(true, 200);
        }

        public async Task<Sha.Response<bool>> MarkAllAsRead(string userId)
        {
            var db = _redisService.GetDb();
            var notifications = await db.SortedSetRangeByScoreAsync($"notifications:{userId}");

            if (!notifications.Any())
            {
                return Sha.Response<bool>.Fail("No notifications found", 404);
            }

            var updatedCount = 0;

            foreach (var serializedNotification in notifications)
            {
                var notification = JsonConvert.DeserializeObject<Models.NotificationDto>(serializedNotification);
                if (notification != null && !notification.Status)
                {
                    // Mark as read
                    notification.Status = true;

                    // Remove the old notification entry
                    await db.SortedSetRemoveAsync($"notifications:{userId}", serializedNotification);

                    // Add the updated notification with the same score
                    var updatedSerializedNotification = JsonConvert.SerializeObject(notification);
                    var score = new DateTimeOffset(notification.CreatedDate).ToUnixTimeSeconds();
                    await db.SortedSetAddAsync($"notifications:{userId}", updatedSerializedNotification, score);

                    updatedCount++;
                }
            }

            return updatedCount > 0
                ? Sha.Response<bool>.Success(true, 200)
                : Sha.Response<bool>.Fail("All notifications are already marked as read", 204);
        }

        public async Task<Sha.Response<List<Models.NotificationDto>>> GetAllCursorPagination(string userId, string latestId, int count = 20)
        {
            var db = _redisService.GetDb();
            userId = $"notifications:{userId}";

            // Define the min and max score depending on the latestId
            double minScore = double.NegativeInfinity;
            double maxScore = double.PositiveInfinity;

            if (!string.IsNullOrEmpty(latestId))
            {
                var latestNotification = await db.SortedSetScoreAsync(userId, latestId);
                if (latestNotification.HasValue)
                {
                    maxScore = latestNotification.Value;
                }
            }

            // Fetch the notifications with a cursor
            var notifications = await db.SortedSetRangeByScoreAsync(
                userId,
                start: minScore,
                stop: maxScore,
                exclude: Exclude.Stop,  // Exclude the max score (i.e., latestId)
                order: Order.Descending,
                take: count
            );

            var notificationList = notifications
                .Select(n => JsonConvert.DeserializeObject<Models.NotificationDto>(n))
                .ToList();

            return notificationList.Any()
                ? Sha.Response<List<Models.NotificationDto>>.Success(notificationList, 200)
                : Sha.Response<List<Models.NotificationDto>>.Fail("No notifications found", 404);
        }
    }
}
