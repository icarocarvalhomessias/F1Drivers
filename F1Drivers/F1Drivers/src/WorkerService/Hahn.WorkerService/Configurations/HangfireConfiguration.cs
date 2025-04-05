using Hangfire;
using Hangfire.SqlServer;

namespace Hahn.WorkerService.Configurations;

public static class HangfireConfiguration
{
    public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
    {
         services.AddHangfire(config => config
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(configuration.GetConnectionString("HahnHangfire"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true,
            SchemaName = "Hangfire"
        }));

        services.AddHangfireServer();
    }
}
