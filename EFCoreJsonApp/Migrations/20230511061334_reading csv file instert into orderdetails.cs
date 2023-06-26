using CsvHelper;
using EFCoreJsonApp.Models.CsvDataReadModels;
using FastMember;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System.Globalization;

#nullable disable

namespace EFCoreJsonApp.Migrations
{
    /// <inheritdoc />
    public partial class readingcsvfileinstertintoorderdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string path = "./files/orderDetails.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<CsvOrderDetailsEntity>();
            IConfiguration config = new ConfigurationBuilder()
               .AddUserSecrets<Program>()
               .Build();
            using var connection = new SqlConnection(config.GetConnectionString("LocalSqlConnection"));
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction);

            bulkCopy.DestinationTableName = "OrderDetails";
            bulkCopy.ColumnMappings.Add("OrderId", "OrderId");
            bulkCopy.ColumnMappings.Add("ItemName", "ItemName");
            bulkCopy.ColumnMappings.Add("Price", "Price");
            bulkCopy.ColumnMappings.Add("Quantity", "Quantity");

            using var recordReader = ObjectReader.Create(records, "OrderId", "ItemName", "Price", "Quantity");
            bulkCopy.WriteToServer(recordReader);

            transaction.Commit();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
