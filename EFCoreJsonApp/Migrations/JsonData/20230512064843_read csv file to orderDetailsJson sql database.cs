using CsvHelper;
using EFCoreJsonApp.Models.CsvDataReadModels;
using FastMember;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System.Globalization;

#nullable disable

namespace EFCoreJsonApp.Migrations.JsonData
{
    /// <inheritdoc />
    public partial class readcsvfiletoorderDetailsJsonsqldatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string path = "./files/orderDetailWithJson.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<CsvOrderDetailsWithJsonEntity>();
            IConfiguration config = new ConfigurationBuilder()
               .AddUserSecrets<Program>()
               .Build();
            using var connection = new SqlConnection(config.GetConnectionString("LocalSqlConnection"));
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction);

            bulkCopy.DestinationTableName = "OrderWithOrderDetails";
            bulkCopy.ColumnMappings.Add("CustomerName", "CustomerName");
            bulkCopy.ColumnMappings.Add("OrderDate", "OrderDate");
            bulkCopy.ColumnMappings.Add("OrderDetailsJson", "OrderDetailsJson");

            using var recordReader = ObjectReader.Create(records, "CustomerName", "OrderDate", "OrderDetailsJson");
            bulkCopy.WriteToServer(recordReader);

            transaction.Commit();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
