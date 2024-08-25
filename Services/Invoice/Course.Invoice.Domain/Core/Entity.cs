namespace Course.Invoice.Domain.Core;
public class Entity
{
    public string Id { get; private set; }

    public Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
