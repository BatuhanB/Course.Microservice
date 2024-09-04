namespace Course.Invoice.Application.Features.Invoice.Adapters;

using Course.Invoice.Application.Features.Invoice.Dtos.InvoiceCreate;
using Course.Invoice.Domain.Invoice;
public static class CreateInvoiceAddressAdapter
{
    public static Address Map(this Address address, AddressDto addressDto)
    {
        address.Province = addressDto.Province;
        address.Street = addressDto.Street;
        address.District = addressDto.District;
        address.ZipCode = addressDto.ZipCode;
        address.Line = addressDto.Line;

        return address;
    }
}
