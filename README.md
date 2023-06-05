# EfCoreJson
EfCore Support of Json

Following step you need to opt when you clone this repostitory:
1. You need to change url of sql database.
    For that you need to set url in secret file my connection string of sql database is : </br>
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=OrdersDB; Integrated Security=True;"
      }
    }
2. You need to just run update-database command when you clone this repo as following:
- Update-Database -context DataContext
- Update-Database -context JsonDataContext
- when you run above comman database is will created and also three table will be created and you get some data inside it.

Following are basic command which I have used:
- Add-Migration "migration for traditional schema" -context DataContext
- Add-Migration "migration for json column schema" -context JsonDataContext
- Update-Database -context DataContext
- Update-Database -context JsonDataContext

## Benchmark Test Report Link is as follow:
https://github.com/MTechRnd/EfCoreJson/blob/benchmarkTestReport/EFCoreJsonApp/BenchmarkTest/Report%20of%20benchmark%20test%20result.md
