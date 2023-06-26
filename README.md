# EfCoreJson
EfCore Support of Json

The following step, you need to opt for when you clone this repository:
1. You need to change URL of SQL database.
    For that you need to set URL in secret file my connection string of SQL database is : </br>
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=OrdersDB; Integrated Security=True;"
      }
    }
2. You need to just run the update-database command when you clone this repo as follows:
- Update-Database -context DataContext
- Update-Database -context JsonDataContext
- When you run the above commands database will be created and also three tables will be created and you get some data inside it.

The following are basic commands which I have used:
- Add-Migration "migration for traditional schema" -context DataContext
- Add-Migration "migration for json column schema" -context JsonDataContext
- Update-Database -context DataContext
- Update-Database -context JsonDataContext

## Code Summary:
- There are two services. One service is for traditional queries and the second service is for json linq service.
- For the traditional approach, there is two tables. One is Order where Id is the primary key. The second table is OrderDetails where OrderId is a foreign key.
- For json linq approach there is only one table which is orderWithOrderDetails. Where you can find one column which has property nvarchar(max). Here in this column, I have stored data in an array of json(OrderDetails).

### ER Diagrams:
- Relationship between order and orderDetails </br>
![image](https://github.com/MTechRnd/EfCoreJson/assets/123544692/a3f6cf41-17ea-42af-ad7c-c328bc9e2b60)</br>
- Order table column name with Data Type:</br>
![image](https://github.com/MTechRnd/EfCoreJson/assets/123544692/fc98922b-19fa-4295-9b8f-f3a6a4f94c37)

- OrderDetails table column name with Data Type:</br>
![image](https://github.com/MTechRnd/EfCoreJson/assets/123544692/52a27557-f3a5-4bd4-abf7-3adad8e020fa)</br>

- OrderWithOrderDetails table column name with Data Type: </br>
![image](https://github.com/MTechRnd/EfCoreJson/assets/123544692/c0fc7fe0-d82a-4709-8b1e-cbfb22bac9fe)

## Benchmark Test Report Link is as follows:
https://github.com/MTechRnd/EfCoreJson/blob/main/EFCoreJsonApp/BenchmarkTest/Report%20of%20benchmark%20test%20result.md
