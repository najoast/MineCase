using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using MineCase.Buffers;
using MineCase.Gateway.Network;
using MineCase.Protocol;
using MineCase.Server;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Orleans;
using Orleans.Hosting;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;

namespace MineCase.Gateway
{
    partial class Program
    {
        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<ConnectionRouter>();
            services.AddSingleton<IPacketCompress, PacketCompress>();
            services.AddTransient<ClientSession>();
            services.AddHostedService<ConnectionRouter>();

            services.AddOrleansClient(builder =>
            {
                builder.UseLocalhostClustering();
                builder.UseMongoDBClient(context.Configuration.GetSection("persistenceOptions")["connectionString"]);
                builder.UseMongoDBClustering(builder =>
                {
                    builder.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                });
                builder.Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "MineCaseService";
                });
            });

            ConfigureObjectPools(services);
        }

        private static void ConfigureObjectPools(IServiceCollection services)
        {
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<ObjectPool<UncompressedPacket>>(s =>
            {
                var provider = s.GetRequiredService<ObjectPoolProvider>();
                return provider.Create<UncompressedPacket>();
            });
            services.AddSingleton<IBufferPool<byte>>(s => new BufferPool<byte>(ArrayPool<byte>.Shared));
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, false);
        }

        private static void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
        }

        private static Assembly[] SelectAssemblies()
        {
            var assemblies = new List<Assembly>();
            assemblies.AddInterfaces();
            return assemblies.ToArray();
        }
    }
}
