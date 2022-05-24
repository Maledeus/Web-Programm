using Microsoft.EntityFrameworkCore.Migrations;

namespace WifiSD.Persistence.Migrations
{
    public partial class Added_Values_And_Ratings2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Dramaqueen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Drama");
        }
    }
}
