using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreJsonApp.Migrations
{
    /// <inheritdoc />
    public partial class fluentapijsondatacontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderWithOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    OrderDetailsJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderWithOrderDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderWithOrderDetails");
        }
    }
}
