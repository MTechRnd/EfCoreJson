using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using EFCoreJsonApp.Data;
using EFCoreJsonApp.Models.Order;
using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.Orders;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using EFCoreJsonApp.Models.OrderWithOrderDetailJson;
using EFCoreJsonApp.Services;
using EFCoreJsonApp.Services.JsonService;
using EFCoreJsonApp.Services.JsonUsingLinqService;
using EFCoreJsonApp.Services.TraditionalService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CodeDom;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Extensions;
using EFCoreJsonApp.BenchmarkTest;

namespace EFCoreJsonApp
{
    class Program
    {
        private static async Task Main(string[] args)
        {

            var summary = BenchmarkRunner.Run<MyBenchmark>();

            //string projectRoot = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            //string filePath = Path.Combine(projectRoot, "output.txt");
            //using (var writer = new StreamWriter(filePath))
            //{
            //    foreach (var report in summary.Reports)
            //    {
            //        writer.WriteLine($"Method: {report.BenchmarkCase.DisplayInfo}");
            //        writer.WriteLine($"Mean: {report.ResultStatistics.Mean} ");
            //        writer.WriteLine($"StdDev: {report.ResultStatistics.StandardDeviation} ");
            //        writer.WriteLine();
            //    }
            //    writer.Flush();
            //}

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

            var order = new OrderEntity
            {
                CustomerName = "Smitesh",
                OrderDate = DateTime.Now,
                OrderDetails = new List<OrderDetailEntity>
                {
                    new OrderDetailEntity()
                    {
                        ItemName = "Black Pen",
                        Price = 250.00f,
                        Quantity = 3
                    },
                    new OrderDetailEntity()
                    {
                        ItemName = "Pencil Box",
                        Price = 250.00f,
                        Quantity = 3
                    },
                    new OrderDetailEntity()
                    {
                        ItemName = "Color Box",
                        Price = 890.00f,
                        Quantity = 1
                    },
                    new OrderDetailEntity()
                    {
                        ItemName = "Graphs",
                        Price = 100.00f,
                        Quantity = 5
                    },
                    new OrderDetailEntity()
                    {
                        ItemName = "Eraser",
                        Price = 30.00f,
                        Quantity = 5
                    },
                    new OrderDetailEntity()
                    {
                        ItemName = "Chocolates",
                        Price = 300.00f,
                        Quantity = 2
                    },
                    new OrderDetailEntity()
                    {
                        ItemName = "White Pen Box",
                        Price = 900.00f,
                        Quantity = 2
                    },
                    new OrderDetailEntity()
                    {
                        ItemName = "Stickers",
                        Price = 100.00f,
                        Quantity = 4
                    }
                }
            };
            //await traditionalService.InsertOrderDetailsAsync(order);


            //List<OrderDetailUpdateDto> OrderDetailsDto = new List<OrderDetailUpdateDto>
            //    {
            //        new OrderDetailUpdateDto(new Guid("8a72d749-7fff-ed11-9f09-f46b8c8f0ef6"),400.00f,3),
            //        new OrderDetailUpdateDto(new Guid("8972d749-7fff-ed11-9f09-f46b8c8f0ef6"),500.00f,2)
            //    };
            //var updateOrderDetailsTraditional = new OrderUpdateDto(new Guid("8872d749-7fff-ed11-9f09-f46b8c8f0ef6"), "smitesh maniya", OrderDetailsDto);
            //var res15 = await traditionalService.UpdateOrderDetailsAsync(updateOrderDetailsTraditional);
            //Console.WriteLine("result of update: " + res15);

            //var res16 = await traditionalService.DeleteOrdersWithOrderId(new Guid("8ea19749-c9fb-ed11-9f09-f46b8c8f0ef6"));
            //Console.WriteLine("res of delete" + res16);


            Console.WriteLine("Json Linq query:");
            var jsonLinqService = serviceProvider.GetService<IJsonUsingLinqService>();
            //var resJsonLinq1 = await jsonLinqService.GetAllDataAsync();
            //var resJsonLinq2 = await jsonLinqService.GetDataForSingleCustomerAsync(idJson);
            //var resJsonLinq3 = await jsonLinqService.GetDataForMultipleCustomerAsync(customerIdsJson);
            //var resJsonLinq4 = await jsonLinqService.TotalOrdersOfCustomerAsync(idJson);
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
            var orderWithOrderDetails = new OrderWithOrderDetailEntity()
            {
                CustomerName = "Smitesh",
                OrderDate = DateTime.Now,
                OrderDetailsJson = new List<OrderDetailsJson>
                {
                    new OrderDetailsJson()
                    {
                        ItemName = "Black Pen",
                        Price = 250.00f,
                        Quantity = 3,
                        Total = 750.00f
                    },
                    new OrderDetailsJson()
                    {
                        ItemName = "Pencil Box",
                        Price = 250.00f,
                        Quantity = 3,
                        Total = 750.00f
                    },
                    new OrderDetailsJson()
                    {
                        ItemName = "Color Box",
                        Price = 890.00f,
                        Quantity = 1,
                        Total = 890.00f
                    },
                    new OrderDetailsJson()
                    {
                        ItemName = "Graphs",
                        Price = 100.00f,
                        Quantity = 5,
                        Total = 500.00f
                    },
                    new OrderDetailsJson()
                    {
                        ItemName = "Eraser",
                        Price = 30.00f,
                        Quantity = 5,
                        Total = 150.00f
                    },
                    new OrderDetailsJson()
                    {
                        ItemName = "Chocolates",
                        Price = 300.00f,
                        Quantity = 2,
                        Total = 600.00f
                    },
                    new OrderDetailsJson()
                    {
                        ItemName = "White Pen Box",
                        Price = 900.00f,
                        Quantity = 2,
                        Total = 1800.00f
                    },
                    new OrderDetailsJson()
                    {
                        ItemName = "Stickers",
                        Price = 100.00f,
                        Quantity = 4,
                        Total = 400.00f
                    }
                }
            };

            //await jsonLinqService.InsertOrderDetailsAsync(orderWithOrderDetails);

            var OrderDetailsJson = new List<OrderDetailsJsonDto>()
                {
                    new OrderDetailsJsonDto(0, 300, 2),
                    new OrderDetailsJsonDto(1, 400, 2)
                };

            //var updateDetailJson = new OrderWithOrderDetailJsonUpdateDto(new Guid("a3cade7c-71ff-ed11-9f09-f46b8c8f0ef6"), "smitesh maniya", OrderDetailsJson);
            //var resJsonLinq15 = await jsonLinqService.UpdateOrderDetailsAsync(updateDetailJson);
            //Console.WriteLine("res of updated linq" + resJsonLinq15);

            //var res16 = await jsonLinqService.DeleteOrdersWithOrderId(new Guid("ac61b463-cbfb-ed11-9f09-f46b8c8f0ef6"));
            //Console.WriteLine("res of delete linq: "+ res16);


            //Console.WriteLine("Json Raw Query:");
            //var jsonService = serviceProvider.GetService<IJsonService>();
            //var idJson = new Guid("7a7827d2-19fa-ed11-9f08-f46b8c8f0ef6");
            //var customerIdsJson = new List<Guid>
            //{
            //    new Guid("7a7827d2-19fa-ed11-9f08-f46b8c8f0ef6"),
            //    new Guid("977827d2-19fa-ed11-9f08-f46b8c8f0ef6"),
            //    new Guid("708b27d2-19fa-ed11-9f08-f46b8c8f0ef6")
            //};
            //var resJson1 = await jsonService.GetAllDataAsync();
            //var resJson2 = await jsonService.GetDataForSingleCustomerAsync(idJson);
            //var resJson3 = await jsonService.GetDataForMultipleCustomerAsync(customerIdsJson);
            //var resJson4 = await jsonService.TotalOrdersOfCustomerAsync(idJson);
            //var resJson5 = await jsonService.TotalOrdersOfCustomersAsync();
            //var resJson6 = await jsonService.AverageOfPriceAsync();
            //var resJson7 = await jsonService.AverageOfQuantityAsync();
            //var resJson8 = await jsonService.SumOfAllPriceAsync();
            //var resJson9 = await jsonService.SumOfAllQuantityAsync();
            //var resJson10 = await jsonService.GetMaxQuantityByOrderIdAsync(idJson);
            //var resJson11 = await jsonService.GetMinQuantityByOrderIdAsync(idJson);
            //var resJson12 = await jsonService.GetTotalByOrderIdAsync(idJson);
            //var resJson13 = await jsonService.GetMaxPriceByOrderIdAsync(idJson);
            //var resJson14 = await jsonService.GetMinPriceByOrderIdAsync(idJson);
        }

    }
}