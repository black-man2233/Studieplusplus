using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using StudiePlusPlus.Infrastructure;
using StudiePlusPlus.Infrastructure.Persistence;

namespace StudiePlusPlus.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();
        builder.Services.AddInfrastructure(builder.Configuration);

        // JWT authentication – token generation is not yet implemented (AuthController is a placeholder)
        var jwtKey = builder.Configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key is missing from configuration.");

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer           = true,
                    ValidateAudience         = true,
                    ValidateLifetime         = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer              = builder.Configuration["Jwt:Issuer"],
                    ValidAudience            = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                };
            });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("StudiePlusPlus API starting. Environment: {Environment}",
            app.Environment.EnvironmentName);

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            try
            {
                logger.LogInformation("Connecting to database...");
                context.Database.EnsureCreated();
                logger.LogInformation("Database ready.");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Failed to connect to database. Check connection string and that SQL Server is running.");
                throw;
            }
        }

        app.UseSwagger(options =>
        {
            options.RouteTemplate = "openapi/{documentName}.json";
            options.SerializeAsV2 = true;
        });
        app.MapScalarApiReference(options =>
        {
            options
                .WithTitle("Studie ++ API")
                .WithTheme(ScalarTheme.BluePlanet)
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                .WithClassicLayout();
        });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        logger.LogInformation("API ready. Listening on port 8080.");
        app.Run();
    }
}
