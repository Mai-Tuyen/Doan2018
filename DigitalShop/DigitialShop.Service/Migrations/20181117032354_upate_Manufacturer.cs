using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitialShop.Service.Migrations
{
    public partial class upate_Manufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Manufacturer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Manufacturer");
        }
    }
}
