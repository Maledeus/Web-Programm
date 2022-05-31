
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WifiSD.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediumTypes",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 8, nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediumTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false, defaultValue: "Unknown"),
                    Price = table.Column<decimal>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "date", nullable: false),
                    GenreId = table.Column<int>(nullable: false),
                    MediumTypeCode = table.Column<string>(maxLength: 8, nullable: true),
                    Rating = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movies_MediumTypes_MediumTypeCode",
                        column: x => x.MediumTypeCode,
                        principalTable: "MediumTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.SetNull);
                });

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
                    { "BR", "Blue Ray" },
                    { "BR-3D", "Blue Ray 3D" },
                    { "BR-HDR", "Blue Ray High Definition Res." },
                    { "VHS", "VHS" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "MediumTypeCode", "Name", "Price", "Rating", "ReleaseDate" },
                values: new object[] { new Guid("1bd83417-f42a-411d-a2fb-d35c85f5b8dc"), 1, "DVD", "Stirb langsam", 7.90m, (byte)1, new DateTime(1988, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "MediumTypeCode", "Name", "Price", "Rating", "ReleaseDate" },
                values: new object[] { new Guid("0f3924ae-4753-4337-9a6b-fb597b5d85c7"), 4, "BR", "Schlimmer gehts immer", 20.99m, (byte)5, new DateTime(2017, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "MediumTypeCode", "Name", "Price", "Rating", "ReleaseDate" },
                values: new object[] { new Guid("0ee99d8e-26ff-40c4-80ef-3b30ab60ee29"), 3, "BR-3D", "Titanic", 9.90m, (byte)3, new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MediumTypeCode",
                table: "Movies",
                column: "MediumTypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Name",
                table: "Movies",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "MediumTypes");
        }
    }
}
