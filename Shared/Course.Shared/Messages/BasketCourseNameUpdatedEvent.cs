namespace Course.Shared.Messages;
public record BasketCourseNameUpdatedEvent
{
    public string UserId { get; set; }
    public string CourseId { get; set; }
    public string CourseName { get; set; }
}
