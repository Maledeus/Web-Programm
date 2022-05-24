using Microsoft.EntityFrameworkCore.Migrations;

namespace WifiSD.Persistence.Migrations
{
    public partial class Set_MediumType_Null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MediumTypes_MediumTypeCode",
                table: "Movies");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MediumTypes_MediumTypeCode",
                table: "Movies",
                column: "MediumTypeCode",
                principalTable: "MediumTypes",
                principalColumn: "Code",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MediumTypes_MediumTypeCode",
                table: "Movies");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MediumTypes_MediumTypeCode",
                table: "Movies",
                column: "MediumTypeCode",
                principalTable: "MediumTypes",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
