using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using EFCoreJsonApp.Data;
using EFCoreJsonApp.Services;
using EFCoreJsonApp.Services.JsonService;
using EFCoreJsonApp.Services.JsonUsingLinqService;
using EFCoreJsonApp.Services.TraditionalService;

namespace EFCoreJsonApp
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class MyBenchmark
    {
        private ITraditionalService _traditionalService;
        private IJsonService _jsonService;
        private IJsonUsingLinqService _jsonUsingLinqService;
        private DataContext _dataContext;
        private JsonDataContext _jsonDataContext;
        private List<Guid> _guidsOfTraditional;
        private List<Guid> _guidsOfJson;
        private Guid _guidTraditional;
        private Guid _guidJson;

        [GlobalSetup]
        public void Setup()
        {
            _dataContext = new DataContext();
            _traditionalService = new TraditionalService(_dataContext);
            _jsonDataContext =new JsonDataContext();
            _jsonService = new JsonService(_jsonDataContext);
            _jsonUsingLinqService = new JsonUsingLinqService(_jsonDataContext);
            _guidsOfTraditional = new List<Guid>
            {
                new Guid("bd031315-41f5-ed11-9f05-f46b8c8f0ef6"),
                new Guid("190c1315-41f5-ed11-9f05-f46b8c8f0ef6"),
                new Guid("421b1315-41f5-ed11-9f05-f46b8c8f0ef6")
            };
            _guidsOfJson = new List<Guid>
            {
                new Guid("7a7827d2-19fa-ed11-9f08-f46b8c8f0ef6"),
                new Guid("977827d2-19fa-ed11-9f08-f46b8c8f0ef6"),
                new Guid("708b27d2-19fa-ed11-9f08-f46b8c8f0ef6")
            };
            _guidTraditional = new Guid("09fa1215-41f5-ed11-9f05-f46b8c8f0ef6");
            _guidJson = new Guid("127527d2-19fa-ed11-9f08-f46b8c8f0ef6");
        }

        [Benchmark(Baseline = true)]
        public async Task TraditionalBenchmark()
        {
            //var res1 = await _traditionalService.GetAllDataAsync();
            //var res2 = await _traditionalService.GetDataForSingleCustomerAsync(_guidTraditional);
            //var res3 = await _traditionalService.GetDataForMultipleCustomerAsync(_guidsOfTraditional);
            //var res4 = await _traditionalService.TotalOrdersOfCustomerAsync(_guidTraditional);
            var res5 = await _traditionalService.TotalOrdersOfCustomersAsync();
            //var res6 = await _traditionalService.AverageOfPriceAsync();
            //var res7 = await _traditionalService.AverageOfQuantityAsync();
            //var res8 = await _traditionalService.SumOfAllPriceAsync();
            //var res9 = await _traditionalService.SumOfAllQuantityAsync();
            //var res10 = await _traditionalService.GetMaxQuantityByOrderIdAsync(_guidTraditional);
            //var res11 = await _traditionalService.GetMinQuantityByOrderIdAsync(_guidTraditional);
            //var res12 = await _traditionalService.GetTotalByOrderIdAsync(_guidTraditional);
            //var res13 = await _traditionalService.GetMaxPriceByOrderIdAsync(_guidTraditional);
            //var res14 = await _traditionalService.GetMinPriceByOrderIdAsync(_guidTraditional);
        }

        [Benchmark]
        public async Task JsonRawQueryBenchmark()
        {
            //var resJson1 = await _jsonService.GetAllDataAsync();
            //var resJson2 = await _jsonService.GetDataForSingleCustomerAsync(_guidJson);
            //var resJson3 = await _jsonService.GetDataForMultipleCustomerAsync(_guidsOfJson);
            //var resJson4 = await _jsonService.TotalOrdersOfCustomerAsync(_guidJson);
            var resJson5 = await _jsonService.TotalOrdersOfCustomersAsync();
            //var resJson6 = await _jsonService.AverageOfPriceAsync();
            //var resJson7 = await _jsonService.AverageOfQuantityAsync();
            //var resJson8 = await _jsonService.SumOfAllPriceAsync();
            //var resJson9 = await _jsonService.SumOfAllQuantityAsync();
            //var resJson10 = await _jsonService.GetMaxQuantityByOrderIdAsync(_guidJson);
            //var resJson11 = await _jsonService.GetMinQuantityByOrderIdAsync(_guidJson);
            //var resJson12 = await _jsonService.GetTotalByOrderIdAsync(_guidJson);
            //var resJson13 = await _jsonService.GetMaxPriceByOrderIdAsync(_guidJson);
            //var resJson14 = await _jsonService.GetMinPriceByOrderIdAsync(_guidJson);
        }

        [Benchmark]
        public async Task JsonLinqBenchmark()
        {
            //var resJson1 = await _jsonUsingLinqService.GetAllDataAsync();
            //var resJson2 = await  _jsonUsingLinqService.GetDataForSingleCustomerAsync(_guidJson);
            //var resJson3 = await  _jsonUsingLinqService.GetDataForMultipleCustomerAsync(_guidsOfJson);
            //var resJson4 =  _jsonUsingLinqService.TotalOrdersOfCustomerAsync(_guidJson);
            var resJson5 = _jsonUsingLinqService.TotalOrdersOfCustomersAsync();
            //var resJson6 =  _jsonUsingLinqService.AverageOfPriceAsync();
            //var resJson7 =  _jsonUsingLinqService.AverageOfQuantityAsync();
            //var resJson8 =  _jsonUsingLinqService.SumOfAllPriceAsync();
            //var resJson9 = _jsonUsingLinqService.SumOfAllQuantityAsync();
            //var resJson10 = await _jsonUsingLinqService.GetMaxQuantityByOrderIdAsync(_guidJson);
            //var resJson11 = _jsonUsingLinqService.GetMinQuantityByOrderIdAsync(_guidJson);
            //var resJson12 = _jsonUsingLinqService.GetTotalByOrderIdAsync(_guidJson);
            //var resJson13 = _jsonUsingLinqService.GetMaxPriceByOrderIdAsync(_guidJson);
            //var resJson14 = _jsonUsingLinqService.GetMinPriceByOrderIdAsync(_guidJson);

        }
    }
}
