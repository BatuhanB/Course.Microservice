using Course.Invoice.Domain.Core;

namespace Course.Invoice.Domain.Invoice;
public class Customer : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Address Address { get; private set; }
    public Customer()
    {
        
    }
    public Customer(string firstName, string lastName, Address address)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
    }
}
