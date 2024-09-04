using Course.Invoice.Api.Middlewares;
using Course.Invoice.Application;
using Course.Invoice.Application.Features.Invoice.Consumers;
using Course.Invoice.Infrastructure.Data;
using Course.Invoice.Presentation;
using Course.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var presentationAssembly = typeof(AssemblyReference).Assembly;

builder.Services.AddApplicationDependencies(builder.Configuration);
builder.Services.AddPresentationDependencies(builder.Configuration);
   
builder.Services.AddExceptionHandler<ExceptionHandler>();
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
}).AddApplicationPart(presentationAssembly);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateInvoiceCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        
        cfg.Host(builder.Configuration["RabbitMQ:URL"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:UserName"]!);
            host.Password(builder.Configuration["RabbitMQ:Password"]!);
        });
        cfg.ReceiveEndpoint("create-invoiceq", e =>
        {
            e.ConfigureConsumer<CreateInvoiceCommandConsumer>(context);
        });
    });
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await ApplyMigrations(app.Services);
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.WebRootPath, "invoices")),
    RequestPath = "/invoices"
});

app.UseExceptionHandler(_ => { });

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task ApplyMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();

    await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await dbContext.Database.MigrateAsync();
}