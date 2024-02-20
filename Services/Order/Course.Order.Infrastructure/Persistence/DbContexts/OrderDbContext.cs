using Microsoft.EntityFrameworkCore;

namespace Course.Order.Infrastructure.Persistence.DbContexts;
public class OrderDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "ordering";

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
    public DbSet<Domain.OrderAggregate.OrderItem> OrderItems { get; set; }
    public DbSet<Domain.OrderAggregate.Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.OrderAggregate.Order>().ToTable(nameof(Orders), DEFAULT_SCHEMA);
        modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().ToTable(nameof(OrderItems), DEFAULT_SCHEMA);
        modelBuilder.Entity<Domain.OrderAggregate.OrderItem>().Property(x=>x.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Domain.OrderAggregate.Order>().OwnsOne(x => x.Address).WithOwner();

        base.OnModelCreating(modelBuilder);
    }
}