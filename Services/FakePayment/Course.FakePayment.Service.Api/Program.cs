using Course.FakePayment.Service.Api.CommandSender;
using Course.FakePayment.Service.Api.Models;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IOrderMessageCommandSender, OrderMessageCommandSender>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:URL"], "/", host =>
        {
            host.Username(builder.Configuration["RabbitMQ:UserName"]!);
            host.Password(builder.Configuration["RabbitMQ:Password"]!);
        });
        cfg.ConfigureEndpoints(context);
    });
});


var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Token:Issuer"];
        options.Audience = builder.Configuration["Token:Audience"];
        options.MapInboundClaims = false;
    });

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
