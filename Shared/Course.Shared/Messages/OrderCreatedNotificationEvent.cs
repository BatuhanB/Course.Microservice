namespace Course.Shared.Messages;
public class OrderCreatedNotificationEvent
{
    public string CourseName { get; set; }
    public string CourseOwnerId { get; set; }
    public DateTime CourseBuyDate { get; set; }
}
