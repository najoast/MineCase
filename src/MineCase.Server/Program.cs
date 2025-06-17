using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MineCase.Serialization.Serializers;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace MineCase.Server
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            var createShardKey = false;
            Serializers.RegisterAll();

            var hostBuilder = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(x => new AutofacServiceProviderFactory(ConfigureAutofac))
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(ConfigureLogging)
                .UseConsoleLifetime()
                .UseOrleans((context, siloBuilder) =>
                {
                    siloBuilder.Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "MineCaseService";
                    })
                    // SchedulingOptions: AllowCallChainReentrancy and PerformDeadlockDetection removed in Orleans 7+
                    .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
                    .UseMongoDBClient(context.Configuration.GetSection("persistenceOptions")["connectionString"])
                    .AddSimpleMessageStreamProvider("JobsProvider")
                    .AddSimpleMessageStreamProvider("TransientProvider")
                    .UseMongoDBReminders(options =>
                    {
                        options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        options.CreateShardKeyForCosmos = createShardKey;
                    })
                    .UseMongoDBClustering(c =>
                    {
                        c.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        c.CreateShardKeyForCosmos = createShardKey;
                    })
                    .UseDashboard()
                    .AddMongoDBGrainStorageAsDefault(c => c.Configure(options =>
                    {
                        options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        options.CreateShardKeyForCosmos = createShardKey;
                    }))
                    .AddMongoDBGrainStorage("PubSubStore", options =>
                    {
                        options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        options.CreateShardKeyForCosmos = createShardKey;
                    });
                });

            var host = hostBuilder.Build();
            Serializers.RegisterAll(host.Services);
            await host.RunAsync();
        }
    }
}