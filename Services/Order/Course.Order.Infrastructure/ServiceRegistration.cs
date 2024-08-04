using Course.Order.Application.Contracts;
using Course.Order.Infrastructure.Persistence.DbContexts;
using Course.Order.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Order.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(x =>
        {
            x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped(typeof(IReadRepository<Domain.OrderAggregate.Order>),
            typeof(ReadRepository<Domain.OrderAggregate.Order, OrderDbContext>));

        services.AddScoped(typeof(IWriteRepository<Domain.OrderAggregate.Order>),
            typeof(WriteRepository<Domain.OrderAggregate.Order, OrderDbContext>));

        services.AddScoped(typeof(IReadRepository<Domain.OrderAggregate.OrderItem>),
            typeof(ReadRepository<Domain.OrderAggregate.OrderItem, OrderDbContext>));

        services.AddScoped(typeof(IWriteRepository<Domain.OrderAggregate.OrderItem>),
            typeof(WriteRepository<Domain.OrderAggregate.OrderItem, OrderDbContext>));
        return services;
    }
}