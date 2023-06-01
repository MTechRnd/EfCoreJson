# EfCoreJson
EfCore Support of Json

You need to just run update-database command when you clone this repo as following:
- Update-Database -context DataContext
- Update-Database -context JsonDataContext

Following are basic command which i have used:
- Add-Migration "migration for traditional schema" -context DataContext
- Add-Migration "migration for json column schema" -context JsonDataContext
- Update-Database -context DataContext
- Update-Database -context JsonDataContext

## Benchmark Test Results:
### Inserting Data:
|               Method |     Mean |    Error |   StdDev | Ratio | RatioSD |      Gen0 |     Gen1 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|------:|--------:|----------:|---------:|----------:|------------:|
| TraditionalBenchmark | 17.43 ms | 14.51 ms | 3.769 ms |  1.00 |    0.00 | 1656.2500 | 203.1250 |  14.92 MB |        1.00 |
|    JsonLinqBenchmark | 20.16 ms | 12.42 ms | 3.227 ms |  1.17 |    0.08 | 2347.6563 |  97.6563 |  21.07 MB |        1.41 |
- Performance Improving Ratio of Traditional query is 14.52%

### Updating Data:
|               Method |     Mean |    Error |   StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
|    JsonLinqBenchmark | 596.2 us | 63.58 us | 16.51 us |  0.86 |    0.03 | 8.7891 |   81.6 KB |        0.95 |
| TraditionalBenchmark | 689.8 us | 40.45 us | 10.51 us |  1.00 |    0.00 | 8.7891 |  86.04 KB |        1.00 |
- Performance Improving Ratio of JsonLinq query is 14.55%
