using Course.Invoice.Application.Invoice.Dtos.InvoiceCreate;
using Course.Invoice.Domain.Invoice;

namespace Course.Invoice.Application.Invoice.Adapters;
public static class CreateInvoiceCustomerAdapter
{
    public static Customer Map(this Customer customer, CustomerDto customerDto)
    {
        customer = 
            new Customer(
                customerDto.UserName, 
                new Address()
                .Map(customerDto.Address));

        return customer;
    }

}
