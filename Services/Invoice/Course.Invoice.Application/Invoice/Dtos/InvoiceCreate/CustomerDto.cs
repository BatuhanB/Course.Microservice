namespace Course.Invoice.Application.Invoice.Dtos.InvoiceCreate;
public class CustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AddressDto Address { get; set; }
}