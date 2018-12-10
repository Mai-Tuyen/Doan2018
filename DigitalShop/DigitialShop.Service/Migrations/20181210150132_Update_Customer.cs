using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitialShop.Service.Migrations
{
    public partial class Update_Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customer",
                nullable: true);
        }
    }
}
