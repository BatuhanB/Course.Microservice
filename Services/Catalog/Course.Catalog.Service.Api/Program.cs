using Course.Catalog.Service.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Token:Issuer"];
        options.Audience = builder.Configuration["Token:Audience"];
        //options.TokenValidationParameters = new TokenValidationParameters
        //{
        //    ValidateIssuer = false,
        //    ValidateAudience = false,
        //    ValidateIssuerSigningKey = true,
        //    IssuerSigningKey = new SymmetricSecurityKey(
        //            Encoding.UTF8.GetBytes(builder.Configuration["Token:SecretKey"])),
        //};
    });

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
