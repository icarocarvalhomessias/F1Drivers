using Hahn.Application;
using Hahn.Domain.Entities;
using Hahn.Infrastructure.Repositories;

namespace Hahn.WebApi.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Driver>, DriverRepository>();
    }
}
