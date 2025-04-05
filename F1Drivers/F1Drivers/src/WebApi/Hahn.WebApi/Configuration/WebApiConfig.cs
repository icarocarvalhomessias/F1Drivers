using Hahn.Application;
using Hahn.Infrastructure;
using Hahn.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Hahn.WebApi.Configuration;

public static class WebApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();

        services.AddExceptionHandler(options =>
        {
            options.ExceptionHandler = async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
            };
        });

        services.AddScoped<IDriverRepository, DriverRepository>();

        services.AddSwaggerGen();
        services.AddSwaggerConfiguration();

        return services;
    }
}
