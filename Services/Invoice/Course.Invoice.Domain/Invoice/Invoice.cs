using Course.Invoice.Domain.Core;

namespace Course.Invoice.Domain.Invoice;
public class Invoice : Entity, IAggregateRoot
{
    public DateTime CreatedDate { get; private set; }
    public Customer Customer { get; private set; }
    public OrderInformation OrderInformation { get; private set; }

    public Invoice()
    {
        
    }
    public Invoice(Customer customer, OrderInformation orderInformation)
    {
        CreatedDate = DateTime.UtcNow;
        Customer = customer;
        OrderInformation = orderInformation;
    }

    public void UpdateOrder(OrderInformation order)
    {
        OrderInformation = order;
    }

    public void UpdateCustomer(Customer customer)
    {
        Customer = customer;
    }
}
