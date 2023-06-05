# Report

Here, you can see all query comparisons between the traditional approach and json linq approach. Here I have created two benchmark methods one is TraditionalBenchmark and the other one is JsonLinqBenchmark.
I have tested all queries one by one and made this report. You can see the test results as follow. I have also added a performance-improving percentage so you can easily see which performed better for that case.

# All Queries are as following:

## Get all data

### Traditional Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], [o0].[Id], [o0].[CreatedAt], [o0].[ItemName], [o0].[OrderId], [o0].[Price], [o0].[Quantity], [o0].[Timestamp], [o0].[Total], [o0].[UpdatedAt] </br>
FROM [Orders] AS [o] </br>
LEFT JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
ORDER BY [o].[Id]

### Json Linq Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$')
FROM [OrderWithOrderDetails] AS [o]

### Benchmark Test Result:
| Method |     Mean |    Error |  StdDev | Ratio | RatioSD |       Gen0 |     Gen1 | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|--------:|------:|--------:|-----------:|---------:|----------:|------------:|
|     JsonLinqBenchmark | 142.4 ms |  9.80 ms | 2.54 ms |  0.94 |    0.03 | 10000.0000 | 750.0000 |  91.67 MB |        1.59 |
|  TraditionalBenchmark | 152.4 ms | 14.05 ms | 3.65 ms |  1.00 |    0.00 |  6250.0000 | 500.0000 |  57.67 MB |        1.00 |

Performance Improving of JsonLinq query is 6.78%

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

### Json Linq Query
 SELECT TOP(1) [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o] </br>
WHERE [o].[Id] = @__id_0
### Benchmark Test Result
|                Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|     JsonLinqBenchmark | 178.2 us | 16.42 us | 4.26 us |  0.99 |    0.04 | 1.9531 |  20.29 KB |        0.96 |
|  TraditionalBenchmark | 180.2 us | 13.87 us | 3.60 us |  1.00 |    0.00 | 1.9531 |  21.09 KB |        1.00 |  

Performance Improving of JsonLinq query is 1.12%

## Get data for multiple customer

### Traditional Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], [o0].[Id], [o0].[CreatedAt], [o0].[ItemName], [o0].[OrderId], [o0].[Price], [o0].[Quantity], [o0].[Timestamp], [o0].[Total], [o0].[UpdatedAt] </br>
 FROM [Orders] AS [o] </br>
 LEFT JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
 WHERE [o].[Id] IN ('fcfb1215-41f5-ed11-9f05-f46b8c8f0ef6', 'b6fd1215-41f5-ed11-9f05-f46b8c8f0ef6', '01fa1215-41f5-ed11-9f05-f46b8c8f0ef6') </br>
 ORDER BY [o].[Id]

### Json Linq Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o] </br>
WHERE [o].[Id] IN ('7a7827d2-19fa-ed11-9f08-f46b8c8f0ef6', '977827d2-19fa-ed11-9f08-f46b8c8f0ef6', '708b27d2-19fa-ed11-9f08-f46b8c8f0ef6')

### Benchmark Test Result:
|                Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|     JsonLinqBenchmark | 284.0 us | 21.26 us | 5.52 us |  0.92 |    0.03 | 5.8594 |  57.63 KB |        1.21 |
|  TraditionalBenchmark | 310.3 us | 17.43 us | 4.53 us |  1.00 |    0.00 | 4.8828 |  47.71 KB |        1.00 |

Performance Improving of JsonLinq query is 8.85%

## Total orders for given customer

### Traditional Query
SELECT COUNT(*) </br>
FROM [Orders] AS [o] </br>
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
WHERE [o].[Id] = @__id_0
                          
### Json Linq Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o] </br>
WHERE [o].[Id] = @__id_0

### Benchmark Test Result:
|                Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|     JsonLinqBenchmark | 158.4 us | 14.14 us | 2.19 us |  0.91 |    0.04 | 1.7090 |  16.76 KB |        1.07 |
|  TraditionalBenchmark | 173.3 us | 20.01 us | 5.20 us |  1.00 |    0.00 | 1.4648 |  15.67 KB |        1.00 |

Performance Improving of JsonLinq query is 8.98%

## Total orders for all customer

### Traditional Query
SELECT [o].[Id], COUNT(*) AS [TotalOrder]
FROM [Orders] AS [o]
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId]
GROUP BY [o].[Id]

### Json Linq Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o]

### Benchmark Test Result:
|                Method |       Mean |      Error |    StdDev | Ratio | RatioSD |      Gen0 |      Gen1 |    Gen2 | Allocated | Alloc Ratio |
|---------------------- |-----------:|-----------:|----------:|------:|--------:|----------:|----------:|--------:|----------:|------------:|
|  TraditionalBenchmark |   9.725 ms |  0.1423 ms | 0.0220 ms |  1.00 |    0.00 |  312.5000 |  109.3750 | 46.8750 |   2.72 MB |        1.00 |
|     JsonLinqBenchmark | 118.075 ms | 21.8423 ms | 3.3801 ms | 12.14 |    0.37 | 8000.0000 | 2000.0000 |       - |  72.36 MB |       26.59 |

Performance Improving of Traditional query is 169.56%

## Average of all price 

### Traditional Query
SELECT CAST(AVG([o0].[Price]) AS float(24)) </br>
FROM [Orders] AS [o] </br>
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId]

### Json Linq Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o]

### Benchmark Test Result:
|                Method |      Mean |     Error |   StdDev | Ratio | RatioSD |      Gen0 |   Allocated | Alloc Ratio |
|---------------------- |----------:|----------:|---------:|------:|--------:|----------:|------------:|------------:|
|  TraditionalBenchmark |  25.02 ms |  3.933 ms | 1.021 ms |  1.00 |    0.00 |         - |    12.56 KB |        1.00 |
|     JsonLinqBenchmark | 121.58 ms | 18.675 ms | 4.850 ms |  4.87 |    0.36 | 8000.0000 | 74077.52 KB |    5,899.47 |

Performance Improving of Traditional query is 131.73%

## Maximum quantity by order id

### Traditional Query
SELECT MAX([o0].[Quantity]) </br>
FROM [Orders] AS [o] </br>
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
WHERE [o].[Id] = @__id_0

### Json Linq Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o] </br>
WHERE [o].[Id] = @__id_0

### Benchmark Test Result:
|                Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|     JsonLinqBenchmark | 163.3 us | 13.93 us | 2.16 us |  0.84 |    0.02 | 1.7090 |  16.87 KB |        1.05 |
|  TraditionalBenchmark | 195.4 us | 41.14 us | 6.37 us |  1.00 |    0.00 | 1.7090 |  16.11 KB |        1.00 |

Performance Improving of JsonLinq query is 17.90%

## Total by order id

### Traditional Query
SELECT CAST(COALESCE(SUM([o0].[Total]), 0.0E0) AS float(24)) </br>
FROM [Orders] AS [o] </br>
INNER JOIN [OrderDetails] AS [o0] ON [o].[Id] = [o0].[OrderId] </br>
WHERE [o].[Id] = @__id_0

### Json Linq Query
SELECT [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt], JSON_QUERY([o].[OrderDetailsJson],'$') </br>
FROM [OrderWithOrderDetails] AS [o] </br>
WHERE [o].[Id] = @__id_0

### Benchmark Test Result:
|                Method |     Mean |    Error |  StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|---------------------- |---------:|---------:|--------:|------:|--------:|-------:|----------:|------------:|
|     JsonLinqBenchmark | 161.7 us | 16.52 us | 4.29 us |  0.81 |    0.02 | 1.7090 |  16.87 KB |        1.05 |
|  TraditionalBenchmark | 199.7 us | 13.79 us | 3.58 us |  1.00 |    0.00 | 1.7090 |  16.03 KB |        1.00 |

Performance Improving of JsonLinq query is 21.03%

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

### Json Linq Query
SET IMPLICIT_TRANSACTIONS OFF; </br>
SET NOCOUNT ON; </br>
INSERT INTO [OrderWithOrderDetails] ([OrderDetailsJson], [CreatedAt], [CustomerName], [OrderDate], [UpdatedAt]) </br>
 OUTPUT INSERTED.[Id], INSERTED.[Timestamp] </br>
 VALUES (@p0, @p1, @p2, @p3, @p4);

### Benchmark Test Result:
|               Method |     Mean |     Error |   StdDev | Ratio | RatioSD |      Gen0 |     Gen1 | Allocated | Alloc Ratio |
|--------------------- |---------:|----------:|---------:|------:|--------:|----------:|---------:|----------:|------------:|
|    JsonLinqBenchmark | 16.22 ms |  9.500 ms | 2.467 ms |  0.57 |    0.02 | 1937.5000 | 328.1250 |  17.42 MB |        0.68 |
| TraditionalBenchmark | 28.63 ms | 17.297 ms | 4.492 ms |  1.00 |    0.00 | 2835.9375 |  54.6875 |  25.46 MB |        1.00 |

Performance Improving of JsonLinq query is 55.34%
 
## Updating Data

### Traditional Query
SELECT [t].[Id], [t].[CreatedAt], [t].[CustomerName], [t].[OrderDate], [t].[Timestamp], [t].[UpdatedAt], [o0].[Id], [o0].[CreatedAt], [o0].[ItemName], [o0].[OrderId], [o0].[Price], [o0].[Quantity], [o0].[Timestamp], [o0].[Total], [o0].[UpdatedAt] </br>
FROM ( </br>
SELECT TOP(1) [o].[Id], [o].[CreatedAt], [o].[CustomerName], [o].[OrderDate], [o].[Timestamp], [o].[UpdatedAt] </br>
FROM [Orders] AS [o] </br>
WHERE [o].[Id] = @__orderDetailsDto_Id_0 </br>
) AS [t] </br>
LEFT JOIN [OrderDetails] AS [o0] ON [t].[Id] = [o0].[OrderId] </br>
ORDER BY [t].[Id]

### Json Linq Query
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
|    JsonLinqBenchmark | 596.2 us | 63.58 us | 16.51 us |  0.86 |    0.03 | 8.7891 |   81.6 KB |        0.95 |
| TraditionalBenchmark | 689.8 us | 40.45 us | 10.51 us |  1.00 |    0.00 | 8.7891 |  86.04 KB |        1.00 |

Performance Improving of JsonLinq query is 14.56%



