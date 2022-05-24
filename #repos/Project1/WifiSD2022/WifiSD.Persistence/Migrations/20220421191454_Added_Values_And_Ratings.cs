using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WifiSD.Persistence.Migrations
{
    public partial class Added_Values_And_Ratings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Rating",
                table: "Movies",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Horror" },
                    { 3, "Drama" },
                    { 4, "Science Fiction" },
                    { 5, "Comedy" }
                });

            migrationBuilder.InsertData(
                table: "MediumTypes",
                columns: new[] { "Code", "Name" },
                values: new object[,]
                {
                    { "DVD", "Digital Versitale Disc" },
                    { "Br", "Blue Ray" },
                    { "BR-3D", "Blue Ray 3D" },
                    { "BR-HDR", "Blue Ray High Definition Res." },
                    { "VHS", "VHS" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "MediumTypeCode", "Name", "Price", "Rating", "ReleaseDate" },
                values: new object[] { new Guid("63060820-09d0-462a-9c79-33fe570a6c4a"), 4, "BR", "Schlimmer gehts immer", 20.99m, (byte)5, new DateTime(2017, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "MediumTypeCode", "Name", "Price", "Rating", "ReleaseDate" },
                values: new object[] { new Guid("2e5e7441-e295-48f7-ba50-a7cb0f41cf18"), 1, "DVD", "Stirb langsam", 7.90m, (byte)0, new DateTime(1988, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "MediumTypeCode", "Name", "Price", "Rating", "ReleaseDate" },
                values: new object[] { new Guid("ce10e516-a7b4-4763-a617-a3938c2e99db"), 3, "BR-3D", "Titanic", 9.90m, (byte)3, new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MediumTypes",
                keyColumn: "Code",
                keyValue: "Br");

            migrationBuilder.DeleteData(
                table: "MediumTypes",
                keyColumn: "Code",
                keyValue: "BR-HDR");

            migrationBuilder.DeleteData(
                table: "MediumTypes",
                keyColumn: "Code",
                keyValue: "VHS");

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("2e5e7441-e295-48f7-ba50-a7cb0f41cf18"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("63060820-09d0-462a-9c79-33fe570a6c4a"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ce10e516-a7b4-4763-a617-a3938c2e99db"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MediumTypes",
                keyColumn: "Code",
                keyValue: "BR-3D");

            migrationBuilder.DeleteData(
                table: "MediumTypes",
                keyColumn: "Code",
                keyValue: "DVD");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");
        }
    }
}
