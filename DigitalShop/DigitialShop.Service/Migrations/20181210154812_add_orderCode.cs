using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitialShop.Service.Migrations
{
    public partial class add_orderCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Order");
        }
    }
}
