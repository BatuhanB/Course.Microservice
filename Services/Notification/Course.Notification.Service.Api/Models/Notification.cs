namespace Course.Notification.Service.Api.Models;

public class Notification
{
    public string Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public bool Status { get; private set; }
    public string UserId { get; private set; }

    public Notification(string title,string description,string userId)
    {
        Id = Guid.NewGuid().ToString();
        Title = title;
        Description = description;
        CreatedDate = DateTime.Now;
        Status = false;
        UserId = userId;
    }

    public Notification(string id,string title, string description, bool status, string userId,DateTime createdDate)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        UserId = userId;
        CreatedDate = createdDate;
    }

    public Notification()
    {
        
    }
}
