namespace Course.Shared.Messages;
public class CourseNameUpdatedEvent
{
    public string CourseId { get; set; }
    public string CourseName { get; set; }
}
