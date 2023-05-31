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
|    JsonLinqBenchmark | 10.47 ms | 4.787 ms | 1.243 ms |  0.65 |    0.02 | 1210.9375 | 164.0625 |  10.89 MB |        0.73 |
| TraditionalBenchmark | 16.24 ms | 9.282 ms | 2.410 ms |  1.00 |    0.00 | 1656.2500 | 203.1250 |  14.92 MB |        1.00 |

### Updating Data:
|               Method |     Mean |    Error |   StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
|    JsonLinqBenchmark | 596.2 us | 63.58 us | 16.51 us |  0.86 |    0.03 | 8.7891 |   81.6 KB |        0.95 |
| TraditionalBenchmark | 689.8 us | 40.45 us | 10.51 us |  1.00 |    0.00 | 8.7891 |  86.04 KB |        1.00 |
