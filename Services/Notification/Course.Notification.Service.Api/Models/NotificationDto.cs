namespace Course.Notification.Service.Api.Models;

public class NotificationDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Status { get; set; }
    public string UserId { get; set; }
}
