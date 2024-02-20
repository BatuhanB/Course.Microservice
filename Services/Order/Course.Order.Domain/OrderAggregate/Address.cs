using Course.Order.Core;

namespace Course.Order.Domain.OrderAggregate;
//Owned Types
public class Address(string province, string district, string street, string zipCode, string line) : ValueObject
{
    public string Province { get; private set; } = province;
    public string District { get; private set; } = district;
    public string Street { get; private set; } = street;
    public string ZipCode { get; private set; } = zipCode;
    public string Line { get; private set; } = line;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Province; 
        yield return District; 
        yield return Street; 
        yield return ZipCode;    
        yield return Line;    
    }
}
