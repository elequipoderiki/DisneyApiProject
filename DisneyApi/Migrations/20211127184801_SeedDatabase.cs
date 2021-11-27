using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyApi.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "CharacterId", "Age", "History", "Image", "Name", "Weight" },
                values: new object[,]
                {
                    { 6, 14, null, null, "Pluto", 20.00m },
                    { 7, 24, null, null, "Donald", 3.00m },
                    { 10, 24, null, null, "Mickey", 30.00m },
                    { 12, 20, null, null, "Dumbo", 30.00m },
                    { 15, 98, null, null, "Tio rico", 30.00m },
                    { 16, 24, null, null, "Bruno", 30.00m }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Image", "Name" },
                values: new object[,]
                {
                    { 1, null, "Western" },
                    { 2, null, "Cartoon" },
                    { 4, null, "Terror" },
                    { 5, null, "Comedy" },
                    { 10, null, "Adventures" },
                    { 11, null, "Biography" },
                    { 12, null, "Future" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "CreateDate", "GenreId", "Image", "Rating", "Title" },
                values: new object[] { 12, new DateTime(1950, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "4", "Pinocho" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "CreateDate", "GenreId", "Image", "Rating", "Title" },
                values: new object[] { 5, new DateTime(1940, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, "4", "Pluto" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "CreateDate", "GenreId", "Image", "Rating", "Title" },
                values: new object[] { 6, new DateTime(1994, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, "4", "El rey león" });

            migrationBuilder.InsertData(
                table: "Playings",
                columns: new[] { "CharacterId", "MovieId" },
                values: new object[,]
                {
                    { 7, 5 },
                    { 10, 5 },
                    { 16, 5 },
                    { 16, 6 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "CharacterId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "CharacterId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "CharacterId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Playings",
                keyColumns: new[] { "CharacterId", "MovieId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "Playings",
                keyColumns: new[] { "CharacterId", "MovieId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "Playings",
                keyColumns: new[] { "CharacterId", "MovieId" },
                keyValues: new object[] { 16, 5 });

            migrationBuilder.DeleteData(
                table: "Playings",
                keyColumns: new[] { "CharacterId", "MovieId" },
                keyValues: new object[] { 16, 6 });

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "CharacterId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "CharacterId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Characters",
                keyColumn: "CharacterId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "MovieId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 2);
        }
    }
}
