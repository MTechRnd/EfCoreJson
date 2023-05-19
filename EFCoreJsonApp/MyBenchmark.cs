using BenchmarkDotNet.Attributes;
using EFCoreJsonApp.Data;
using EFCoreJsonApp.Services;
using EFCoreJsonApp.Services.JsonService;
using EFCoreJsonApp.Services.TraditionalService;

namespace EFCoreJsonApp
{
    [MemoryDiagnoser]
    public class MyBenchmark
    {
        private ITraditionalService _traditionalService;
        private IJsonService _jsonService;
        private DataContext _dataContext;
        private JsonDataContext _jsonDataContext;
        private List<Guid> _guidsOfTraditional;
        private List<Guid> _guidsOfJson;

        [GlobalSetup]
        public void Setup()
        {
            _traditionalService = new TraditionalService(_dataContext);
            _jsonService = new JsonService(_jsonDataContext);
            _guidsOfTraditional = new List<Guid>
            {
                new Guid("8aed95bc-c0ef-ed11-9f03-f46b8c8f0ef6"),
                new Guid("8eed95bc-c0ef-ed11-9f03-f46b8c8f0ef6"),
                new Guid("5ef095bc-c0ef-ed11-9f03-f46b8c8f0ef6"),
                new Guid("62f095bc-c0ef-ed11-9f03-f46b8c8f0ef6")
            };
            _guidsOfJson = new List<Guid>
            {
                new Guid("73dd068a-91f0-ed11-9f03-f46b8c8f0ef6"),
                new Guid("a6dd068a-91f0-ed11-9f03-f46b8c8f0ef6"),
                new Guid("badd068a-91f0-ed11-9f03-f46b8c8f0ef6"),
                new Guid("9cdd068a-91f0-ed11-9f03-f46b8c8f0ef6")
            };
        }

        [Benchmark(Baseline = true)]
        public void TraditionalBenchmark()
        {
            var res1 = _traditionalService.GetAllData();
            //var res2 = _traditionalService.GetDataForSingleCustomer(new Guid("8bed95bc-c0ef-ed11-9f03-f46b8c8f0ef6"));
            //var res3 = _traditionalService.GetDataForMultipleCustomer(_guidsOfTraditional);
            //var res4 = _traditionalService.AggregateOperation();
            //var res5 = _traditionalService.TotalOrdersOfCustomer(new Guid("8eed95bc-c0ef-ed11-9f03-f46b8c8f0ef6"));
            //var res6 = _traditionalService.TotalOrdersOfCustomers();
        }

        [Benchmark]
        public void JsonBenchmark()
        {
            var resJson1 = _jsonService.GetAllData();
            //var resJson2 = _jsonService.GetDataForSingleCustomer(new Guid("73dd068a-91f0-ed11-9f03-f46b8c8f0ef6"));
            //var resJson3 = _jsonService.GetDataForMultipleCustomer(_guidsOfJson);
            //var resJson4 = _jsonService.AggregateOperation();
            //var resJson5 = _jsonService.TotalOrdersOfCustomer(new Guid("badd068a-91f0-ed11-9f03-f46b8c8f0ef6"));
            //var resJson6 = _jsonService.TotalOrdersOfCustomers();
        }
    }
}
