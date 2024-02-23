using Course.Order.Core;

namespace Course.Order.Domain.OrderAggregate;
//Owned Types
public class Address : ValueObject
{
    public Address()
    {
        
    }
    public Address(string province, string district, string street, string zipCode, string line)
    {
        Province = province;
        District = district;
        Street = street;
        ZipCode = zipCode;
        Line = line;
    }

    public string Province { get; private set; }
    public string District { get; private set; }
    public string Street { get; private set; }
    public string ZipCode { get; private set; }
    public string Line { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Province; 
        yield return District; 
        yield return Street; 
        yield return ZipCode;    
        yield return Line;    
    }
}
