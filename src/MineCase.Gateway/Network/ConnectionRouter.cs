using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using MineCase.Buffers;
using MineCase.Protocol;
using MineCase.Server.Settings;
using Orleans;

namespace MineCase.Gateway.Network
{
    internal class ConnectionRouter(
        IClusterClient grainFactory,
        ILogger<ConnectionRouter> logger,
        IServiceProvider serviceProvider)
        : IHostedService
    {
        private readonly ILogger _logger = logger;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var grain = grainFactory.GetGrain<IServerSettings>(0);
                var settings = await grain.GetSettings();
                IPAddress ip = IPAddress.Parse(settings.ServerIp);
                int port = (int)settings.ServerPort;

                var listener = new TcpListener(new IPEndPoint(ip, port));
                listener.Start();
                _logger.LogInformation("ConnectionRouter started.");
                while (!cancellationToken.IsCancellationRequested)
                {
                    DispatchIncomingClient(await listener.AcceptTcpClientAsync(cancellationToken), cancellationToken);
                }
                listener.Stop();
            }
            catch (FormatException)
            {
                _logger.LogError($"The configuration of gateway have an incorrect format.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void DispatchIncomingClient(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Incoming connection from {ClientRemoteEndPoint}.", tcpClient.Client.RemoteEndPoint);
                using var session = ActivatorUtilities.CreateInstance<ClientSession>(serviceProvider, tcpClient);
                await session.Startup(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
            }
        }
    }
}
