using Course.Order.API.Middlewares;
using Course.Order.Application;
using Course.Order.Application.Consumers;
using Course.Order.Domain.OrderAggregate;
using Course.Order.Infrastructure;
using Course.Order.Infrastructure.Persistence.DbContexts;
using Course.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddApplicationDependency();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Token:Issuer"];
        options.Audience = builder.Configuration["Token:Audience"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero, // Remove clock skew
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
        };
    });

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateOrderMessageCommandConsumer>();
    x.AddConsumer<CourseUpdatedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:URL"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:UserName"]!);
            host.Password(builder.Configuration["RabbitMQ:Password"]!);
        });
        cfg.ReceiveEndpoint("update-order-item-nameq", e =>
        {
            e.ConfigureConsumer<CourseUpdatedEventConsumer>(context);
        });
        cfg.ReceiveEndpoint("create-orderq", e =>
        {
            e.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
        });
    });
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using IServiceScope scope = MigrateDataIfNotExists(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

static IServiceScope MigrateDataIfNotExists(WebApplication app)
{
    var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    dbContext.Database.Migrate();

    if (!dbContext.Orders.Any())
    {
        var order1 = new Order("b27560fb-8385-4524-b052-faddced6a12d", new Address("Test Province", "Test Disctrict", "Test Street", "00000", "Test Line 1"));
        var order2 = new Order("b27560fb-8385-4524-b052-faddced6a12d", new Address("Test Province", "Test Disctrict", "Test Street", "11111", "Test Line 2"));
        order1.AddOrderItem(Guid.NewGuid().ToString(), "Test Product", 123, "", "b27560fb-8385-4524-b052-faddced6a12d");
        order2.AddOrderItem(Guid.NewGuid().ToString(), "Test Product 2", 223, "", "b27560fb-8385-4524-b052-faddced6a12d");
        dbContext.Orders.Add(order1);
        dbContext.Orders.Add(order2);
    }

    return scope;
}