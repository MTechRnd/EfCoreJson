using BenchmarkDotNet.Running;
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
            BenchmarkRunner.Run<MyBenchmark>();
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
            var traditionalService = serviceProvider.GetService<ITraditionalService>();
            var id = new Guid("09fa1215-41f5-ed11-9f05-f46b8c8f0ef6");
            var customerIds = new List<Guid>
            {
                new Guid("fcfb1215-41f5-ed11-9f05-f46b8c8f0ef6"),
                new Guid("b6fd1215-41f5-ed11-9f05-f46b8c8f0ef6"),
                new Guid("01fa1215-41f5-ed11-9f05-f46b8c8f0ef6")
            };
            Console.WriteLine("Traditional query:");
            //var res1 = await traditionalService.GetAllDataAsync();
            //var res2 = await traditionalService.GetDataForSingleCustomerAsync(id);
            //var res3 = await traditionalService.GetDataForMultipleCustomerAsync(customerIds);
            //var res4 = await traditionalService.TotalOrdersOfCustomerAsync(id);
            //var res5 = await traditionalService.TotalOrdersOfCustomersAsync();
            //var res6 = await traditionalService.AverageOfPriceAsync();
            //var res7 = await traditionalService.AverageOfQuantityAsync();
            //var res8 = await traditionalService.SumOfAllPriceAsync();
            //var res9 = await traditionalService.SumOfAllQuantityAsync();
            //var res10 = await traditionalService.GetMaxQuantityByOrderIdAsync(id);
            //var res11 = await traditionalService.GetMinQuantityByOrderIdAsync(id);
            //var res12 = await traditionalService.GetTotalByOrderIdAsync(id);
            //var res13 = await traditionalService.GetMaxPriceByOrderIdAsync(id);
            //var res14 = await traditionalService.GetMinPriceByOrderIdAsync(id);


            Console.WriteLine("Json Raw Query:");
            var jsonService = serviceProvider.GetService<IJsonService>();
            var idJson = new Guid("7a7827d2-19fa-ed11-9f08-f46b8c8f0ef6");
            var customerIdsJson = new List<Guid>
            {
                new Guid("7a7827d2-19fa-ed11-9f08-f46b8c8f0ef6"),
                new Guid("977827d2-19fa-ed11-9f08-f46b8c8f0ef6"),
                new Guid("708b27d2-19fa-ed11-9f08-f46b8c8f0ef6")
            };
            //var resJson1 = await jsonService.GetAllDataAsync();
            //var resJson2 = await jsonService.GetDataForSingleCustomerAsync(idJson);
            //var resJson3 = await jsonService.GetDataForMultipleCustomerAsync(customerIdsJson);
            //var resJson4 = await jsonService.TotalOrdersOfCustomerAsync(idJson);
            var resJson5 = await jsonService.TotalOrdersOfCustomersAsync();
            //var resJson6 = await jsonService.AverageOfPriceAsync();
            //var resJson7 = await jsonService.AverageOfQuantityAsync();
            //var resJson8 = await jsonService.SumOfAllPriceAsync();
            //var resJson9 = await jsonService.SumOfAllQuantityAsync();
            //var resJson10 = await jsonService.GetMaxQuantityByOrderIdAsync(idJson);
            //var resJson11 = await jsonService.GetMinQuantityByOrderIdAsync(idJson);
            //var resJson12 = await jsonService.GetTotalByOrderIdAsync(idJson);
            //var resJson13 = await jsonService.GetMaxPriceByOrderIdAsync(idJson);
            //var resJson14 = await jsonService.GetMinPriceByOrderIdAsync(idJson);

            Console.WriteLine("Json Linq query:");
            var jsonLinqService = serviceProvider.GetService<IJsonUsingLinqService>();
            //var resJsonLinq1 = await jsonLinqService.GetAllDataAsync();
            //var resJsonLinq2 = await jsonLinqService.GetDataForSingleCustomerAsync(idJson);
            //var resJsonLinq3 = await jsonLinqService.GetDataForMultipleCustomerAsync(customerIdsJson);
            //var resJsonLinq4 = await jsonLinqService.TotalOrdersOfCustomerAsync(new Guid("e5715528-43f5-ed11-9f05-f46b8c8f0ef6"));
            //var resJsonLinq5 = await jsonLinqService.TotalOrdersOfCustomersAsync();
            //var resJsonLinq6 = await jsonLinqService.AverageOfPriceAsync();
            //var resJsonLinq7 = await jsonLinqService.AverageOfQuantityAsync();
            //var resJsonLinq8 = await jsonLinqService.SumOfAllPriceAsync();
            //var resJsonLinq9 = await jsonLinqService.SumOfAllQuantityAsync();
            //var resJsonLinq10 = await jsonLinqService.GetMaxQuantityByOrderIdAsync(idJson);
            //var resJsonLinq11 = await jsonLinqService.GetMinQuantityByOrderIdAsync(idJson);
            //var resJsonLinq12 = await jsonLinqService.GetTotalByOrderIdAsync(idJson);
            //var resJsonLinq13 = await jsonLinqService.GetMaxPriceByOrderIdAsync(idJson);
            //var resJsonLinq14 = await jsonLinqService.GetMinPriceByOrderIdAsync(idJson);

        }

    }
}