using EFCoreJsonApp.Data;
using EFCoreJsonApp.Services;
using EFCoreJsonApp.Services.JsonService;
using EFCoreJsonApp.Services.JsonUsingLinqService;
using EFCoreJsonApp.Services.TraditionalService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCoreJsonApp
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            //BenchmarkRunner.Run<MyBenchmark>();
            var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<DataContext>();
                services.AddDbContext<JsonDataContext>();
                services.AddScoped<ITraditionalService, TraditionalService>();
                services.AddScoped<IJsonService, JsonService>();
                services.AddScoped<IJsonUsingLinqService, JsonUsingLinqService>();
            });

            using var host = hostBuilder.Build();
            using var serviceScope = host.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            // Use services
            var myService = serviceProvider.GetService<ITraditionalService>();
            //var res = await myService.GetTotalByOrderId(new Guid("02fa1215-41f5-ed11-9f05-f46b8c8f0ef6"));

            var jsonService = serviceProvider.GetService<IJsonService>();
            //var jsonResult = await jsonService.GetMinPriceByOrderId(new Guid("ffff068a-91f0-ed11-9f03-f46b8c8f0ef6"));

            var jsonLinqService = serviceProvider.GetService<IJsonUsingLinqService>();
            //var josnResultLinq = await jsonLinqService.GetMinPriceByOrderId(new Guid("72dd068a-91f0-ed11-9f03-f46b8c8f0ef6"));
        }

    }
}