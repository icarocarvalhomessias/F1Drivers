using Hahn.Application.Mappings;
using Hahn.WebApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var hostEnvironment = builder.Environment;
        builder.Configuration
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        builder.Services.AddApiConfiguration(builder.Configuration);
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Hahn API",
                Description = "API for Hahn application"
            });
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
        });

        // Chame o método RegisterServices para registrar os serviços
        builder.Services.RegisterServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn API v1");
            });
        }
        // Use CORS
        app.UseCors("AllowAllOrigins");
        app.UseSwaggerConfiguration();
        app.MapControllers();

        app.Run();
    }
}
