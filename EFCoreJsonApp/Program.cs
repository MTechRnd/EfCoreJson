using EFCoreJsonApp.Data;
using EFCoreJsonApp.Services;
using EFCoreJsonApp.Services.JsonService;
using EFCoreJsonApp.Services.TraditionalService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCoreJsonApp
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<DataContext>();
                services.AddDbContext<JsonDataContext>();
                services.AddScoped<ITraditionalService,TraditionalService>();
                services.AddScoped<IJsonService,JsonService>();
            });

            using var host = hostBuilder.Build();
            using var serviceScope = host.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            // Use services
            var myService = serviceProvider.GetService<ITraditionalService>();
            var res = await myService.GetAllData();

            var jsonService = serviceProvider.GetService<IJsonService>();
            var result = await jsonService.GetAllData();
        }

    }
}