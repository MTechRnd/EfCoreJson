﻿# Report

Here, you can see all query comparisons between the traditional approach and Json approach. Here I have created two benchmark methods one is TraditionalBenchmark and the other one is JsonLinqBenchmark.
I have tested all queries one by one and made this report. You can see the test results as follow. I have also added a performance-improving percentage so you can easily see which performed better for that case.

# All Queries are as following:

## Get all data

### Traditional Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], [o0].[Id], [o0].[CreatedAt], [o0].[ItemName], [o0].[OrderId], [o0].[Price], [o0].[Quantity], [o0].[Timestamp], [o0].[Total], [o0].[UpdatedAt] </br>
FROM [Orders] AS [o] </br>
LEFT JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
ORDER BY [o].[Id]

### Json Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$')
FROM [OrderWithOrderDetails] AS [o]

### Benchmark Test Result:
|         Method        |     Mean |    Error |  StdDev | Ratio | RatioSD |       Gen0 |     Gen1 | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|--------:|------:|--------:|-----------:|---------:|----------:|------------:|
|       JsonBenchmark   | 142.4 ms |  9.80 ms | 2.54 ms |  0.94 |    0.03 | 10000.0000 | 750.0000 |  91.67 MB |        1.59 |
|  TraditionalBenchmark | 152.4 ms | 14.05 ms | 3.65 ms |  1.00 |    0.00 |  6250.0000 | 500.0000 |  57.67 MB |        1.00 |

Performance Improving of Json query is 6.78%

## Get single data of customer

### Traditional Query
SELECT [t].[Id], [t].[CreatedAt], [t].[CustomerName], [t].[OrderDate], [t].[Timestamp], [t].[UpdatedAt], [o0].[Id], [o0].[CreatedAt], [o0].[ItemName], [o0].[OrderId], [o0].[Price], [o0].[Quantity], [o0].[Timestamp], [o0].[Total], [o0].[UpdatedAt]  </br>
FROM (  </br>
SELECT TOP(1) [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt]  </br>
FROM [Orders] AS [o]  </br>
WHERE [o].[Id] = @__id_0 </br>
) AS [t] </br>
LEFT JOIN [OrderDetails] AS [o0] ON [t].[Id] = [o0].[OrderId] </br>
ORDER BY [t].[Id]

### Json Query
 SELECT TOP(1) [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o] </br>
WHERE [o].[Id] = @__id_0
### Benchmark Test Result
|                Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|       JsonBenchmark   | 178.2 us | 16.42 us | 4.26 us |  0.99 |    0.04 | 1.9531 |  20.29 KB |        0.96 |
|  TraditionalBenchmark | 180.2 us | 13.87 us | 3.60 us |  1.00 |    0.00 | 1.9531 |  21.09 KB |        1.00 |  

Performance Improving of Json query is 1.12%

## Get data for multiple customers

### Traditional Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], [o0].[Id], [o0].[CreatedAt], [o0].[ItemName], [o0].[OrderId], [o0].[Price], [o0].[Quantity], [o0].[Timestamp], [o0].[Total], [o0].[UpdatedAt] </br>
 FROM [Orders] AS [o] </br>
 LEFT JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
 WHERE [o].[Id] IN ('fcfb1215-41f5-ed11-9f05-f46b8c8f0ef6', 'b6fd1215-41f5-ed11-9f05-f46b8c8f0ef6', '01fa1215-41f5-ed11-9f05-f46b8c8f0ef6') </br>
 ORDER BY [o].[Id]

### Json Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o] </br>
WHERE [o].[Id] IN ('7a7827d2-19fa-ed11-9f08-f46b8c8f0ef6', '977827d2-19fa-ed11-9f08-f46b8c8f0ef6', '708b27d2-19fa-ed11-9f08-f46b8c8f0ef6')

### Benchmark Test Result:
|                Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|       JsonBenchmark   | 284.0 us | 21.26 us | 5.52 us |  0.92 |    0.03 | 5.8594 |  57.63 KB |        1.21 |
|  TraditionalBenchmark | 310.3 us | 17.43 us | 4.53 us |  1.00 |    0.00 | 4.8828 |  47.71 KB |        1.00 |

Performance Improving of Json query is 8.85%

## Total orders for given customer

### Traditional Query
SELECT COUNT(*) </br>
FROM [Orders] AS [o] </br>
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
WHERE [o].[Id] = @__id_0
                          
### Json Query
SELECT count(*) as TotalOrderByCustomerId </br>
FROM OrderWithOrderDetails </br>
CROSS APPLY OPENJSON(OrderDetailsJson) AS item </br>
WHERE Id = 'e27827d2-19fa-ed11-9f08-f46b8c8f0ef6' </br>

### Benchmark Test Result:
|               Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|        JsonBenchmark | 174.1 us |  8.36 us | 2.17 us |  0.97 |    0.03 | 1.7090 |  17.06 KB |        1.11 |
| TraditionalBenchmark | 178.9 us | 22.02 us | 5.72 us |  1.00 |    0.00 | 1.4648 |  15.37 KB |        1.00 |

Performance Improving of Json query is 2.72%

## Total orders for all customers

### Traditional Query
SELECT [o].[Id], COUNT(*) AS [TotalOrder] </br>
FROM [Orders] AS [o] </br>
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
GROUP BY [o].[Id] </br>

### Json Query
SELECT Id, </br>
COUNT(*) AS TotalOrder </br>
FROM OrderWithOrderDetails </br>
CROSS APPLY OPENJSON(OrderDetailsJson) AS items </br>
WHERE ISJSON(OrderDetailsJson) > 0 </br>
GROUP BY Id;

### Benchmark Test Result:
|               Method |      Mean |     Error |   StdDev | Ratio | RatioSD |     Gen0 |     Gen1 |    Gen2 | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|---------:|------:|--------:|---------:|---------:|--------:|----------:|------------:|
| TraditionalBenchmark |  10.71 ms |  2.939 ms | 0.763 ms |  1.00 |    0.00 | 296.8750 | 109.3750 | 46.8750 |   2.72 MB |        1.00 |
|        JsonBenchmark | 190.59 ms | 23.055 ms | 5.987 ms | 17.88 |    1.52 |        - |        - |       - |   2.72 MB |        1.00 |

Performance Improving of Traditional query is 178.72%

## Average of all price

### Traditional Query
SELECT CAST(AVG([o0].[Price]) AS float(24)) </br>
FROM [Orders] AS [o] </br>
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId]

### Json Query
SELECT AVG(item.Price) AS AverageOfPrice </br>
FROM OrderWithOrderDetails </br>
CROSS APPLY OPENJSON(OrderDetailsJson) </br>
WITH (Price decimal(10,2) '$.Price') AS item

### Benchmark Test Result:
|               Method |      Mean |     Error |   StdDev | Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|---------:|------:|--------:|----------:|------------:|
| TraditionalBenchmark |  25.65 ms |  5.427 ms | 1.409 ms |  1.00 |    0.00 |  12.56 KB |        1.00 |
|        JsonBenchmark | 106.74 ms | 13.253 ms | 2.051 ms |  4.10 |    0.15 |  16.01 KB |        1.28 |

Performance Improving of Traditional query is 122.50%

## Maximum quantity by order id

### Traditional Query
SELECT MAX([o0].[Quantity]) </br>
FROM [Orders] AS [o] </br>
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
WHERE [o].[Id] = @__id_0

### Json Query
SELECT MAX(item.Price) AS MaximumPrice </br>
FROM OrderWithOrderDetails </br>
CROSS APPLY OPENJSON(OrderDetailsJson) </br>
WITH (Price Decimal(10,2) '$.Price') AS item </br>
WHERE Id = 'e27827d2-19fa-ed11-9f08-f46b8c8f0ef6'

### Benchmark Test Result:
|               Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|        JsonBenchmark | 168.0 us | 15.09 us | 3.92 us |  0.90 |    0.02 | 1.7090 |  17.69 KB |        1.10 |
| TraditionalBenchmark | 186.4 us | 31.19 us | 8.10 us |  1.00 |    0.00 | 1.7090 |  16.11 KB |        1.00 |

Performance Improving of Json query is 10.38%

## Total by order id

### Traditional Query
SELECT CAST(COALESCE(SUM([o0].[Total]), 0.0E0) AS float(24)) </br>
FROM [Orders] AS [o] </br>
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
WHERE [o].[Id] = @__id_0

### Json Query
SELECT SUM(item.Total) AS TotalByOrderId </br>
FROM OrderWithOrderDetails </br>
CROSS APPLY OPENJSON(OrderDetailsJson) </br>
WITH (Total Decimal(10,2) '$.Total') AS item </br>
WHERE Id = 'e27827d2-19fa-ed11-9f08-f46b8c8f0ef6'

### Benchmark Test Result:
|               Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|        JsonBenchmark | 167.7 us | 20.10 us | 5.22 us |  0.86 |    0.04 | 1.7090 |  17.63 KB |        1.10 |
| TraditionalBenchmark | 195.1 us | 21.86 us | 5.68 us |  1.00 |    0.00 | 1.7090 |  16.03 KB |        1.00 |

Performance Improving of Json query is 15.10%

## Insert Data

### Traditional Query
SET IMPLICIT_TRANSACTIONS OFF; </br>
SET NOCOUNT ON; </br>
INSERT INTO [Orders] ([CreatedAt], [CustomerName], [OrderDate], [UpdatedAt]) </br>
OUTPUT INSERTED.[Id], INSERTED.[Timestamp] </br>
VALUES (@p0, @p1, @p2, @p3);
      
SET IMPLICIT_TRANSACTIONS OFF; </br>
SET NOCOUNT ON; </br>
MERGE [OrderDetails] USING ( </br>
VALUES (@p4, @p5, @p6, @p7, @p8, @p9, 0),
(@p10, @p11, @p12, @p13, @p14, @p15, 1),
(@p16, @p17, @p18, @p19, @p20, @p21, 2)) AS i ([CreatedAt], [ItemName], [OrderId], [Price], [Quantity], [UpdatedAt], _Position) ON 1=0 </br>
WHEN NOT MATCHED THEN </br>
INSERT ([CreatedAt], [ItemName], [OrderId], [Price], [Quantity], [UpdatedAt]) </br>
VALUES (i.[CreatedAt], i.[ItemName], i.[OrderId], i.[Price], i.[Quantity], i.[UpdatedAt]) </br>
OUTPUT INSERTED.[Id], INSERTED.[Timestamp], INSERTED.[Total], i._Position;

### Json Query
SET IMPLICIT_TRANSACTIONS OFF; </br>
SET NOCOUNT ON; </br>
INSERT INTO [OrderWithOrderDetails] ([OrderDetailsJson], [CreatedAt], [CustomerName], [OrderDate], [UpdatedAt]) </br>
 OUTPUT INSERTED.[Id], INSERTED.[Timestamp] </br>
 VALUES (@p0, @p1, @p2, @p3, @p4);

### Benchmark Test Result:
|               Method |     Mean |     Error |   StdDev | Ratio | RatioSD |      Gen0 |     Gen1 | Allocated | Alloc Ratio |
|--------------------- |---------:|----------:|---------:|------:|--------:|----------:|---------:|----------:|------------:|
|      JsonBenchmark   | 16.22 ms |  9.500 ms | 2.467 ms |  0.57 |    0.02 | 1937.5000 | 328.1250 |  17.42 MB |        0.68 |
| TraditionalBenchmark | 28.63 ms | 17.297 ms | 4.492 ms |  1.00 |    0.00 | 2835.9375 |  54.6875 |  25.46 MB |        1.00 |

Performance Improving of Json query is 55.34%
 
## Update Data

### Traditional Query
SELECT [t].[Id], [t].[CreatedAt], [t].[CustomerName], [t].[OrderDate], [t].[Timestamp], [t].[UpdatedAt], [o0].[Id], [o0].[CreatedAt], [o0].[ItemName], [o0].[OrderId], [o0].[Price], [o0].[Quantity], [o0].[Timestamp], [o0].[Total], [o0].[UpdatedAt] </br>
FROM ( </br>
SELECT TOP(1) [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt] </br>
FROM [Orders] AS [o] </br>
WHERE [o].[Id] = @__orderDetailsDto_Id_0 </br>
) AS [t] </br>
LEFT JOIN [OrderDetails] AS [o0] ON [t].[Id] = [o0].[OrderId] </br>
ORDER BY [t].[Id]

### Json Query
SELECT TOP(1) [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o] </br>
WHERE [o].[Id] = @__orderWithOrdderDetailsDto_Id_0 

SET IMPLICIT_TRANSACTIONS OFF; </br>
SET NOCOUNT ON; </br>
UPDATE [OrderWithOrderDetails] SET [OrderDetailsJson] = @p0, [CustomerName] = @p1, [UpdatedAt] = @p2 </br>
OUTPUT INSERTED.[Timestamp] </br>
WHERE [Id] = @p3 AND [Timestamp] = @p4;

### Benchmark Test Result:
|               Method |     Mean |    Error |   StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
|      JsonBenchmark   | 596.2 us | 63.58 us | 16.51 us |  0.86 |    0.03 | 8.7891 |   81.6 KB |        0.95 |
| TraditionalBenchmark | 689.8 us | 40.45 us | 10.51 us |  1.00 |    0.00 | 8.7891 |  86.04 KB |        1.00 |

Performance Improving of Json query is 14.56%