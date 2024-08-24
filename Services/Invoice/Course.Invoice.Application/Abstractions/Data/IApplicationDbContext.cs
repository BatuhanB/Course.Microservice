
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    //DbSet<Invoice> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
