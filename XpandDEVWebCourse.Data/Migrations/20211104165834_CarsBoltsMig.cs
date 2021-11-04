using Microsoft.EntityFrameworkCore.Migrations;

namespace XpandDEVWebCourse.Web.Migrations
{
    public partial class CarsBoltsMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NrBolts",
                table: "Cars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrBolts",
                table: "Cars");
        }
    }
}
