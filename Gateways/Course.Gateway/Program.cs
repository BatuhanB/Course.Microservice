using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
{
    options.Authority = builder.Configuration["Token:Issuer"];
    options.Audience = builder.Configuration["Token:Audience"];
    options.MapInboundClaims = false;
});


builder.Host.ConfigureAppConfiguration((hostingContext,config) =>
{
    config
    .AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json")
    .AddEnvironmentVariables();
});

builder.Services.AddOcelot();

var app = builder.Build();

await app.UseOcelot();

app.Run();
