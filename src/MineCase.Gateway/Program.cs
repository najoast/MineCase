using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MineCase.Gateway
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder.ConfigureAppConfiguration(ConfigureAppConfiguration);
            hostBuilder.ConfigureServices(ConfigureServices);
            hostBuilder.ConfigureLogging(ConfigureLogging);
            hostBuilder.UseConsoleLifetime();

            var host = hostBuilder.Build();
            await host.RunAsync();
        }
    }
}