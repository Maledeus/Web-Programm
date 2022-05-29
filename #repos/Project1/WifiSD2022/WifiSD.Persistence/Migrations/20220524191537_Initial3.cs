using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WifiSD.Persistence.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediumTypes",
                keyColumn: "Code",
                keyValue: "BR");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Drama");

            migrationBuilder.InsertData(
                table: "MediumTypes",
                columns: new[] { "Code", "Name" },
                values: new object[] { "BR", "Blue Ray" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MediumTypes",
                keyColumn: "Code",
                keyValue: "BR");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("63060820-09d0-462a-9c79-33fe570a6c4a"),
                column: "MediumTypeCode",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Dramaqueen");

            migrationBuilder.InsertData(
                table: "MediumTypes",
                columns: new[] { "Code", "Name" },
                values: new object[] { "BR", "Blue Ray" });
        }
    }
}
