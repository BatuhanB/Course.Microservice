using Course.Invoice.Domain.Invoice;
using Microsoft.EntityFrameworkCore;

namespace Course.Invoice.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    private const string DEFAULT_SCHEME = "invoices";
    public DbSet<Domain.Invoice.Invoice> Invoices { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<OrderInformation> OrderInformations { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Domain.Invoice.Invoice>(invoice =>
            {
                invoice.HasKey(x => x.Id);
                invoice.HasOne(x => x.Customer);
                invoice.HasOne(x => x.OrderInformation);
                invoice.ToTable("Invoice",DEFAULT_SCHEME);
            });
        modelBuilder
            .Entity<Customer>(customer =>
            {
                customer.HasKey(x => x.Id);
                customer.ToTable("Customer", DEFAULT_SCHEME);
                customer.OwnsOne(x=>x.Address);
            });
        modelBuilder
            .Entity<OrderInformation>(orderInf =>
            {
                orderInf.HasKey(x => x.Id);
                orderInf
                    .HasMany(x => x.OrderItems)
                    .WithOne()
                    .HasForeignKey(y=>y.OrderInformationId);
                orderInf.ToTable("OrderInformation", DEFAULT_SCHEME);
            });
        modelBuilder
            .Entity<OrderItem>(orderItem =>
            {
                orderItem.HasKey(x => x.Id);
                orderItem
                    .Property(x => x.Price)
                    .HasColumnType("decimal(18,2)");
                orderItem.ToTable("OrderItem", DEFAULT_SCHEME);
            });

        base.OnModelCreating(modelBuilder);
    }
}
