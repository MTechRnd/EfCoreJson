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

## Benchmark Test Results
### Getting All Data:
|               Method |     Mean |   Error |  StdDev | Ratio | RatioSD |       Gen0 |     Gen1 | Allocated | Alloc Ratio |
|--------------------- |---------:|--------:|--------:|------:|--------:|-----------:|---------:|----------:|------------:|
|    JsonLinqBenchmark | 135.7 ms | 9.66 ms | 2.51 ms |  0.88 |    0.02 | 10000.0000 | 750.0000 |  91.67 MB |        1.59 |
| TraditionalBenchmark | 154.5 ms | 3.68 ms | 0.95 ms |  1.00 |    0.00 |  6250.0000 | 500.0000 |  57.67 MB |        1.00 |
- Performance Improving of JsonLinq query is 12.95%

### Getting data for single customer:
|               Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|    JsonLinqBenchmark | 166.7 us | 21.14 us | 5.49 us |  0.89 |    0.03 | 2.1973 |  20.22 KB |        0.96 |
| TraditionalBenchmark | 186.6 us |  8.05 us | 2.09 us |  1.00 |    0.00 | 1.9531 |  20.98 KB |        1.00 |
- Performance Improving of JsonLinq query is 11.26%

### Getting data for multiple customer:
|               Method |     Mean |    Error |   StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
|    JsonLinqBenchmark | 241.6 us | 49.96 us |  7.73 us |  0.88 |    0.06 | 4.3945 |  41.73 KB |        1.11 |
| TraditionalBenchmark | 276.7 us | 44.28 us | 11.50 us |  1.00 |    0.00 | 3.9063 |  37.46 KB |        1.00 |
- Performance Improving of JsonLinq query is 13.54%

### Total Orders of single customer:
|               Method |     Mean |    Error |  StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|--------:|------:|-------:|----------:|------------:|
|    JsonLinqBenchmark | 168.9 us |  6.80 us | 1.77 us |  0.88 | 1.7090 |  16.69 KB |        1.07 |
| TraditionalBenchmark | 191.0 us | 11.22 us | 2.91 us |  1.00 | 1.4648 |   15.6 KB |        1.00 |
- Performance Improving of JsonLinq query is 12.28%

### Total Orders of all customer:
|               Method |      Mean |     Error |   StdDev | Ratio | RatioSD |      Gen0 |      Gen1 |    Gen2 | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|---------:|------:|--------:|----------:|----------:|--------:|----------:|------------:|
| TraditionalBenchmark |  10.57 ms |  1.886 ms | 0.292 ms |  1.00 |    0.00 |  312.5000 |  109.3750 | 46.8750 |   2.72 MB |        1.00 |
|    JsonLinqBenchmark | 121.20 ms | 20.660 ms | 3.197 ms | 11.47 |    0.31 | 8000.0000 | 2000.0000 |       - |  72.36 MB |       26.59 |
- Performance Improving of Traditional query is 167.91%

### Average of all price:
|               Method |      Mean |     Error |   StdDev | Ratio | RatioSD |      Gen0 |   Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|---------:|------:|--------:|----------:|------------:|------------:|
| TraditionalBenchmark |  23.86 ms |  1.441 ms | 0.374 ms |  1.00 |    0.00 |         - |     12.7 KB |        1.00 |
|    JsonLinqBenchmark | 119.85 ms | 15.128 ms | 2.341 ms |  5.04 |    0.15 | 8000.0000 | 74077.63 KB |    5,834.14 |
- Performance Improving of Traditional query is 133.58%

### Maximum quantity by Order id
|               Method |     Mean |    Error |  StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|--------:|------:|-------:|----------:|------------:|
|    JsonLinqBenchmark | 164.2 us | 17.67 us | 4.59 us |  0.83 | 1.7090 |   16.8 KB |        1.03 |
| TraditionalBenchmark | 198.8 us | 14.65 us | 3.80 us |  1.00 | 1.7090 |  16.34 KB |        1.00 |
- Performance Improving of JsonLinq query is 19.06%

### Total by Order id:
|               Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|    JsonLinqBenchmark | 166.9 us |  7.05 us | 1.83 us |  0.82 |    0.02 | 1.7090 |   16.8 KB |        1.05 |
| TraditionalBenchmark | 203.7 us | 10.08 us | 2.62 us |  1.00 |    0.00 | 1.7090 |  16.03 KB |        1.00 |
- Performance Improving of JsonLinq query is 19.85%

### Inserting Data:
|               Method |     Mean |     Error |   StdDev | Ratio | RatioSD |      Gen0 |     Gen1 | Allocated | Alloc Ratio |
|--------------------- |---------:|----------:|---------:|------:|--------:|----------:|---------:|----------:|------------:|
|    JsonLinqBenchmark | 16.22 ms |  9.500 ms | 2.467 ms |  0.57 |    0.02 | 1937.5000 | 328.1250 |  17.42 MB |        0.68 |
| TraditionalBenchmark | 28.63 ms | 17.297 ms | 4.492 ms |  1.00 |    0.00 | 2835.9375 |  54.6875 |  25.46 MB |        1.00 |
- Performance Improving of JsonLinq query is 55.34%

### Updating Data:
|               Method |     Mean |    Error |   StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
|    JsonLinqBenchmark | 596.2 us | 63.58 us | 16.51 us |  0.86 |    0.03 | 8.7891 |   81.6 KB |        0.95 |
| TraditionalBenchmark | 689.8 us | 40.45 us | 10.51 us |  1.00 |    0.00 | 8.7891 |  86.04 KB |        1.00 |
- Performance Improving of JsonLinq query is 14.55%
