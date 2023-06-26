using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreJsonApp.Migrations.JsonData
{
    /// <inheritdoc />
    public partial class addtimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OrderWithOrderDetails",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "OrderWithOrderDetails",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OrderWithOrderDetails",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OrderWithOrderDetails");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "OrderWithOrderDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OrderWithOrderDetails");
        }
    }
}
