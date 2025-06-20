using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using MineCase.Buffers;
using MineCase.Gateway.Network;
using MineCase.Protocol;
using System.Buffers;
using System.IO;
using Orleans;
using Orleans.Hosting;
using Orleans.Configuration;

namespace MineCase.Gateway
{
    internal static partial class Program
    {
        private static async Task Main(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder.ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("config.json", false, false);
            });
            hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddLogging();
                services.AddSingleton<ConnectionRouter>();
                services.AddSingleton<IPacketCompress, PacketCompress>();
                services.AddTransient<ClientSession>();
                services.AddHostedService<ConnectionRouter>();

                services.AddOrleansClient(builder =>
                {
                    builder.UseMongoDBClient(context.Configuration.GetSection("persistenceOptions")["connectionString"]);
                    builder.UseMongoDBClustering(options =>
                    {
                        options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                    });
                    builder.Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "MineCaseService";
                    });
                });

                // Object pools
                services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
                services.AddSingleton<ObjectPool<UncompressedPacket>>(s =>
                {
                    var provider = s.GetRequiredService<ObjectPoolProvider>();
                    return provider.Create<UncompressedPacket>();
                });
                services.AddSingleton<IBufferPool<byte>>(s => new BufferPool<byte>(ArrayPool<byte>.Shared));
            });
            hostBuilder.ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
            });
            hostBuilder.UseConsoleLifetime();

            var host = hostBuilder.Build();
            await host.RunAsync();
        }
    }
}