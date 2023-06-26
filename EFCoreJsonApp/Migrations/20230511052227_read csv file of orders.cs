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
    public partial class readcsvfileoforders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string path = "./files/orders.csv";
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<CsvOrderEntity>();
            IConfiguration config = new ConfigurationBuilder()
               .AddUserSecrets<Program>()
               .Build();
            using var connection = new SqlConnection(config.GetConnectionString("LocalSqlConnection"));
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction);

            bulkCopy.DestinationTableName = "Orders";
            bulkCopy.ColumnMappings.Add("Id", "Id");
            bulkCopy.ColumnMappings.Add("CustomerName", "CustomerName");
            bulkCopy.ColumnMappings.Add("OrderDate", "OrderDate");

            using var recordReader = ObjectReader.Create(records, "Id", "CustomerName", "OrderDate");
            bulkCopy.WriteToServer(recordReader);

            transaction.Commit();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
