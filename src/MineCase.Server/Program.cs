using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MineCase.Serialization.Serializers;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Threading.Tasks;
using MineCase.Abstractions.Constants;
using Orleans.Providers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using MineCase.Server.Settings;
using Microsoft.IO;

namespace MineCase.Server;

internal static partial class Program
{
    private static async Task Main(string[] args)
    {
        const bool createShardKey = false;
        Serializers.RegisterAll();

        var hostBuilder = Host.CreateDefaultBuilder(args);
        hostBuilder.UseServiceProviderFactory(x => new AutofacServiceProviderFactory(builder =>
        {
            var assemblies = new List<Assembly>();
            assemblies
                .AddEngine()
                .AddInterfaces()
                .AddGrains();
            builder.RegisterAssemblyModules(assemblies.ToArray());
        }));
        hostBuilder.ConfigureAppConfiguration(builder =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, false);
        });
        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddOptions();
            services.AddLogging();
            services.AddSingleton<RecyclableMemoryStreamManager>();
            services.Configure<PersistenceOptions>(context.Configuration.GetSection("persistenceOptions"));
        });
        //hostBuilder.ConfigureLogging(loggingBuilder => loggingBuilder.AddConsole());
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

            // 3.x 写法
            //siloBuilder.AddSimpleMessageStreamProvider("JobsProvider");
            //siloBuilder.AddSimpleMessageStreamProvider("TransientProvider");
            // 7.x 写法：
            // https://learn.microsoft.com/en-us/dotnet/orleans/migration-guide
            // BroadcastChannel 改动过大，先用 MemoryStreams 了
            // siloBuilder.AddBroadcastChannel(StreamProviders.JobsProvider, options => options.FireAndForgetDelivery = false);
            // siloBuilder.AddBroadcastChannel(StreamProviders.TransientProvider, options => options.FireAndForgetDelivery = false);
            siloBuilder.AddMemoryStreams<DefaultMemoryMessageBodySerializer>(StreamProviders.JobsProvider, c => c.ConfigurePartitioning());
            siloBuilder.AddMemoryStreams<DefaultMemoryMessageBodySerializer>(StreamProviders.TransientProvider, c => c.ConfigurePartitioning());

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