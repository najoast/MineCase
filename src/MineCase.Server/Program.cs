using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MineCase.Serialization.Serializers;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;


namespace MineCase.Server;

partial class Program
{
    static async Task Main(string[] args)
    {
        var createShardKey = false;
        Serializers.RegisterAll();

        var hostBuilder = Host.CreateDefaultBuilder(args);
        hostBuilder.UseServiceProviderFactory(x => new AutofacServiceProviderFactory(ConfigureAutofac));
        hostBuilder.ConfigureAppConfiguration(ConfigureAppConfiguration);
        hostBuilder.ConfigureServices(ConfigureServices);
        //hostBuilder.ConfigureLogging(ConfigureLogging);
        hostBuilder.UseConsoleLifetime();
        hostBuilder.UseOrleans((context, siloBuilder) =>
        {
            //Orleans.Hosting.ISiloBuilder builder;
            siloBuilder.UseDashboard();
            siloBuilder.Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "MineCaseService";
            });
            // SchedulingOptions: AllowCallChainReentrancy and PerformDeadlockDetection removed in Orleans 7+
            siloBuilder.ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000);
            siloBuilder.UseMongoDBClient(context.Configuration.GetSection("persistenceOptions")["connectionString"]);
            //siloBuilder.AddSimpleMessageStreamProvider("JobsProvider");
            //siloBuilder.AddSimpleMessageStreamProvider("TransientProvider");
            siloBuilder.UseMongoDBReminders(options =>
            {
                options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                options.CreateShardKeyForCosmos = createShardKey;
            });
            siloBuilder.UseMongoDBClustering(c =>
            {
                c.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                c.CreateShardKeyForCosmos = createShardKey;
            });
            siloBuilder.AddMongoDBGrainStorageAsDefault(c => c.Configure(options =>
            {
                options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                options.CreateShardKeyForCosmos = createShardKey;
            }));
            siloBuilder.AddMongoDBGrainStorage("PubSubStore", options =>
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