using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PruebaAppApi;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AplicationServices.Helpers.Logger;
using AplicationServices.Application.Contracts.Helpers;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);


startup.ConfigureServices(builder.Services);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(builder.Configuration["KeyJwt"])),
                   ClockSkew = TimeSpan.Zero
               });


builder.Services.AddTransient<ILoggerServices, LoggerService>();

var app = builder.Build();


startup.Configure(app, app.Environment);

app.Run();

    
