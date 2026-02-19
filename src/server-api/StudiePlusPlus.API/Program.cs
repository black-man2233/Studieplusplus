using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using StudiePlusPlus.Infrastructure;
using StudiePlusPlus.Infrastructure.Persistence;
namespace StudiePlusPlus.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddControllers();
        builder.Services.AddInfrastructure(builder.Configuration);

        var app = builder.Build();

        // Apply migrations automatically
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
        }

        if (app.Environment.IsDevelopment())
        {
            // app.UseSwagger();
            // app.UseSwaggerUI();
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
        app.MapControllers();
        app.Run();
    }
}