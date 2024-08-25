using Course.Invoice.Domain.Invoice;
using Microsoft.EntityFrameworkCore;

namespace Course.Invoice.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Domain.Invoice.Invoice> Invoices { get; }
    DbSet<Customer> Customers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}