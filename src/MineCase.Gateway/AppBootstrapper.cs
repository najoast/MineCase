﻿using Microsoft.Extensions.Configuration;
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
using Microsoft.Extensions.Hosting;

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
            services.AddOrleansMultiClient(builder =>
            {
                builder.AddClient(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "MineCaseService";
                    options.Configure = c =>
                    {
                        // c.UseLocalhostClustering(gatewayPort: 30000);
                        c.UseMongoDBClient(context.Configuration.GetSection("persistenceOptions")["connectionString"]);
                        c.UseMongoDBClustering(options =>
                        {
                            options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        });
                    };
                    options.SetServiceAssembly(SelectAssemblies());
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
