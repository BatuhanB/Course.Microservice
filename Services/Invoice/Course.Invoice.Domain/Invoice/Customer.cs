using Course.Invoice.Domain.Core;

namespace Course.Invoice.Domain.Invoice;
public class Customer : ValueObject
{
    public string UserName { get; private set; }
    public Address Address { get; private set; }
    public Customer()
    {
        
    }
    public Customer(string userName, Address address)
    {
        UserName = userName;
        Address = address;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserName;
        yield return Address;
    }
}
