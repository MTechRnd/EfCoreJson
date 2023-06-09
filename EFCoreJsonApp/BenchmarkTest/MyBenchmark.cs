﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
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

namespace EFCoreJsonApp.BenchmarkTest
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class MyBenchmark
    {
        private ITraditionalService _traditionalService;
        private IJsonUsingLinqService _jsonUsingLinqService;
        private DataContext _dataContext;
        private JsonDataContext _jsonDataContext;
        private List<Guid> _guidsOfTraditional;
        private List<Guid> _guidsOfJson;
        private Guid _guidTraditional;
        private Guid _guidJson;
        private OrderEntity _orderTraditional;
        private List<OrderDetailEntity> _orderDetailsTraditional;
        private OrderWithOrderDetailEntity _orderWithOrderDetailsLinq;
        private List<float> Prices;
        private List<int> Quantities;
        private Random RandomIndex;
        private List<string> CustomerNames;

        [GlobalSetup]
        public async void Setup()
        {
            _dataContext = new DataContext();
            _traditionalService = new TraditionalService(_dataContext);
            _jsonDataContext = new JsonDataContext();
            _jsonUsingLinqService = new JsonUsingLinqService(_jsonDataContext);
            _guidsOfTraditional = new List<Guid>
            {
                new Guid("bd031315-41f5-ed11-9f05-f46b8c8f0ef6"),
                new Guid("190c1315-41f5-ed11-9f05-f46b8c8f0ef6"),
                new Guid("421b1315-41f5-ed11-9f05-f46b8c8f0ef6"),
                new Guid("73131315-41f5-ed11-9f05-f46b8c8f0ef6"),
                new Guid("5f131315-41f5-ed11-9f05-f46b8c8f0ef6")
            };
            _guidsOfJson = new List<Guid>
            {
                new Guid("7a7827d2-19fa-ed11-9f08-f46b8c8f0ef6"),
                new Guid("977827d2-19fa-ed11-9f08-f46b8c8f0ef6"),
                new Guid("708b27d2-19fa-ed11-9f08-f46b8c8f0ef6"),
                new Guid("da8727d2-19fa-ed11-9f08-f46b8c8f0ef6"),
                new Guid("1f8827d2-19fa-ed11-9f08-f46b8c8f0ef6"),
            };
            _guidTraditional = new Guid("2efa1215-41f5-ed11-9f05-f46b8c8f0ef6");
            _guidJson = new Guid("e27827d2-19fa-ed11-9f08-f46b8c8f0ef6");

            Prices = new List<float>
            {
                550.50f, 780.34f, 235.50f, 200.00f, 499.00f, 1299.00f, 500.00f, 260.80f, 300.90f, 400.00f
            };

            Quantities = new List<int>
            {
                4, 5, 8, 9, 1, 2, 3, 10, 7, 6
            };

            CustomerNames = new List<string>
            {
                "Milan",
                "Jay",
                "Rahul",
                "Vijay",
                "Nainesh",
                "Amit",
                "Vishal",
                "Raj"
            };

            RandomIndex = new Random();
            DeleteOtherRecordsOfOrderEntity();
            DeleteOtherRecordsOfOrderWithOrderDetailsEntity();
        }
        // Delete records in orders entity which is above 10,000 before running a benchmark test. It will delete referenced records.
        public void DeleteOtherRecordsOfOrderEntity()
        {
            var totalRecords = _dataContext.Orders.Count();
            var removeRecors = totalRecords - 10000;
            if (removeRecors > 0)
            {
                var recordsToDelete = _dataContext.Orders.OrderByDescending(o => o.Id).Take(removeRecors).Select(o => o.Id).ToList();
                var records = _dataContext.Orders.Where(o => recordsToDelete.Contains(o.Id)).ToList();
                _dataContext.Orders.RemoveRange(records);
                _dataContext.SaveChanges();
            }
        }
        // Delete records in orderWithOrderDetail above 10,000 before running a benchmark test. It will delete referenced records.
        public void DeleteOtherRecordsOfOrderWithOrderDetailsEntity()
        {
            var totalRecords = _jsonDataContext.OrderWithOrderDetails.Count();
            var removeRecors = totalRecords - 10000;
            if (removeRecors > 0)
            {
                var recordsToDelete = _jsonDataContext.OrderWithOrderDetails.OrderByDescending(o => o.Id).Take(removeRecors).Select(o => o.Id).ToList();
                var records = _jsonDataContext.OrderWithOrderDetails.Where(o => recordsToDelete.Contains(o.Id)).ToList();
                _jsonDataContext.OrderWithOrderDetails.RemoveRange(records);
                _jsonDataContext.SaveChanges();
            }
        }

        [Benchmark(Baseline = true)]
        public async Task TraditionalBenchmark()
        {
            var res1 = await _traditionalService.GetAllDataAsync();
            //var res2 = await _traditionalService.GetDataForSingleCustomerAsync(_guidTraditional);
            //var res3 = await _traditionalService.GetDataForMultipleCustomerAsync(_guidsOfTraditional);
            //var res4 = await _traditionalService.TotalOrdersOfCustomerAsync(_guidTraditional);
            //var res5 = await _traditionalService.TotalOrdersOfCustomersAsync();
            //var res6 = await _traditionalService.AverageOfPriceAsync();
            //var res7 = await _traditionalService.AverageOfQuantityAsync();
            //var res8 = await _traditionalService.SumOfAllPriceAsync();
            //var res9 = await _traditionalService.SumOfAllQuantityAsync();
            //var res10 = await _traditionalService.GetMaxQuantityByOrderIdAsync(_guidTraditional);
            //var res11 = await _traditionalService.GetMinQuantityByOrderIdAsync(_guidTraditional);
            //var res12 = await _traditionalService.GetTotalByOrderIdAsync(_guidTraditional);
            //var res13 = await _traditionalService.GetMaxPriceByOrderIdAsync(_guidTraditional);
            //var res14 = await _traditionalService.GetMinPriceByOrderIdAsync(_guidTraditional);

            //var order = new OrderEntity
            //{
            //    CustomerName = "Smitesh",
            //    OrderDate = DateTime.Now,
            //    OrderDetails = new List<OrderDetailEntity>
            //    {
            //        new OrderDetailEntity()
            //        {
            //            ItemName = "Show Piece",
            //            Price = 250.00f,
            //            Quantity = 3
            //        },
            //        new OrderDetailEntity()
            //        {
            //            ItemName = "Sho Piece of Camle",
            //            Price = 250.00f,
            //            Quantity = 3
            //        },
            //        new OrderDetailEntity()
            //        {
            //            ItemName = "Show Piece",
            //            Price = 250.00f,
            //            Quantity = 3
            //        }
            //    }
            //};
            //await _traditionalService.InsertOrderDetailsAsync(order);

            //int PriceRandomizer = RandomIndex.Next(0, Prices.Count);
            //int QuantityRandomizer = RandomIndex.Next(0, Prices.Count);
            //int NameRandomizer = RandomIndex.Next(0, CustomerNames.Count);
            //List<OrderDetailUpdateDto> OrderDetailsDto = new List<OrderDetailUpdateDto>
            //{
            //    new OrderDetailUpdateDto(new Guid("e7ba8d99-9aff-ed11-9f09-f46b8c8f0ef6"),Prices[PriceRandomizer],Quantities[QuantityRandomizer]),
            //    new OrderDetailUpdateDto(new Guid("e4ba8d99-9aff-ed11-9f09-f46b8c8f0ef6"),Prices[PriceRandomizer],Quantities[QuantityRandomizer])
            //};
            //var updateOrderDetailsTraditional = new OrderUpdateDto(new Guid("dfba8d99-9aff-ed11-9f09-f46b8c8f0ef6"), CustomerNames[NameRandomizer], OrderDetailsDto);
            //await _traditionalService.UpdateOrderDetailsAsync(updateOrderDetailsTraditional);
        }

        [Benchmark]
        public async Task JsonBenchmark()
        {
            var resJson1 = await _jsonUsingLinqService.GetAllDataAsync();
            //var resJson2 = await _jsonUsingLinqService.GetDataForSingleCustomerAsync(_guidJson);
            //var resJson3 = await _jsonUsingLinqService.GetDataForMultipleCustomerAsync(_guidsOfJson);
            //var resJson4 = await _jsonUsingLinqService.TotalOrdersOfCustomerAsync(_guidJson);
            //var resJson5 = await _jsonUsingLinqService.TotalOrdersOfCustomersAsync();
            //var resJson6 = await _jsonUsingLinqService.AverageOfPriceAsync();
            //var resJson7 = await _jsonUsingLinqService.AverageOfQuantityAsync();
            //var resJson8 = await _jsonUsingLinqService.SumOfAllPriceAsync();
            //var resJson9 = await _jsonUsingLinqService.SumOfAllQuantityAsync();
            //var resJson10 = await _jsonUsingLinqService.GetMaxQuantityByOrderIdAsync(_guidJson);
            //var resJson11 = await _jsonUsingLinqService.GetMinQuantityByOrderIdAsync(_guidJson);
            //var resJson12 = await _jsonUsingLinqService.GetTotalByOrderIdAsync(_guidJson);
            //var resJson13 = await _jsonUsingLinqService.GetMaxPriceByOrderIdAsync(_guidJson);
            //var resJson14 = await _jsonUsingLinqService.GetMinPriceByOrderIdAsync(_guidJson);


            //var orderWithOrderDetails = new OrderWithOrderDetailEntity()
            //{
            //    CustomerName = "Smitesh",
            //    OrderDate = DateTime.Now,
            //    OrderDetailsJson = new List<OrderDetailsJson>
            //    {
            //        new OrderDetailsJson()
            //        {
            //            ItemName = "Show Piece",
            //            Price = 250.00f,
            //            Quantity = 3,
            //            Total = 750.00f
            //        },
            //        new OrderDetailsJson()
            //        {
            //            ItemName = "Sho Piece of Camle",
            //            Price = 250.00f,
            //            Quantity = 3,
            //            Total = 750.00f
            //        },
            //        new OrderDetailsJson()
            //        {
            //            ItemName = "Show Piece",
            //            Price = 250.00f,
            //            Quantity = 3,
            //            Total = 750.00f
            //        }
            //    }
            //};

            //await _jsonUsingLinqService.InsertOrderDetailsAsync(orderWithOrderDetails);

            //int PriceRandomizer = RandomIndex.Next(0, Prices.Count);
            //int QuantityRandomizer = RandomIndex.Next(0, Prices.Count);
            //int NameRandomizer = RandomIndex.Next(0, CustomerNames.Count);
            //var OrderDetailsJson = new List<OrderDetailsJsonDto>()
            //{
            //    new OrderDetailsJsonDto(4, Prices[PriceRandomizer],Quantities[QuantityRandomizer]),
            //    new OrderDetailsJsonDto(7, Prices[PriceRandomizer],Quantities[QuantityRandomizer])
            //};

            //var updateDetailJson = new OrderWithOrderDetailJsonUpdateDto(new Guid("367c7749-9bff-ed11-9f09-f46b8c8f0ef6"), CustomerNames[NameRandomizer], OrderDetailsJson);
            //await _jsonUsingLinqService.UpdateOrderDetailsAsync(updateDetailJson);

        }

        //[Benchmark]
        //public async Task JsonRawQueryBenchmark()
        //{
        //    //var resJson1 = await _jsonService.GetAllDataAsync();
        //    //var resJson2 = await _jsonService.GetDataForSingleCustomerAsync(_guidJson);
        //    //var resJson3 = await _jsonService.GetDataForMultipleCustomerAsync(_guidsOfJson);
        //    //var resJson4 = await _jsonService.TotalOrdersOfCustomerAsync(_guidJson);
        //    //var resJson5 = await _jsonService.TotalOrdersOfCustomersAsync();
        //    //var resJson6 = await _jsonService.AverageOfPriceAsync();
        //    //var resJson7 = await _jsonService.AverageOfQuantityAsync();
        //    //var resJson8 = await _jsonService.SumOfAllPriceAsync();
        //    //var resJson9 = await _jsonService.SumOfAllQuantityAsync();
        //    //var resJson10 = await _jsonService.GetMaxQuantityByOrderIdAsync(_guidJson);
        //    //var resJson11 = await _jsonService.GetMinQuantityByOrderIdAsync(_guidJson);
        //    //var resJson12 = await _jsonService.GetTotalByOrderIdAsync(_guidJson);
        //    //var resJson13 = await _jsonService.GetMaxPriceByOrderIdAsync(_guidJson);
        //    //var resJson14 = await _jsonService.GetMinPriceByOrderIdAsync(_guidJson);
        //}

    }
}
