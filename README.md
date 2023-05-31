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
 - Method: MyBenchmark.JsonLinqBenchmark
   - Mean: 19.86 ms
   - StdDev: 12.55
   - Ratio: 1.21

- Method: MyBenchmark.TraditionalBenchmark
  - Mean: 16.60 ms
  - StdDev: 9.750 ms
  - Ratio: 1.0

### Updating Data:
- Method: MyBenchmark.JsonLinqBenchmark
  - Mean: 610.2 us
  - StdDev: 22.10 us
  - Ratio: 0.88

- Method: MyBenchmark.TraditionalBenchmark
  - Mean: 690.9 us
  - StdDev: 5.20 ms
  - Ratio: 1.0
