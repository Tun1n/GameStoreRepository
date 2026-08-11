using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Apex Legends", "/ApexLegends.jpg", false });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Dead Island", "/DeadIsland.jpg", true });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Fortnite", "/Fortnite.webp", true });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "LEGO Star Wars", "/LegoStarWars.jpg", false });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "LOL", "/LOL.webp", true });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Naruto", "/Naruto.jpg", false });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "NBA 2K26", "/NBA2K26.jpg", true });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "PEAK", "/PEAK.webp", true });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Rocket League", "/RocketLeague.jpg", true });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Subnautica 2", "/subnautica2.webp", true });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Tekken 7", "/tekken7.jpg", true });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Undertale", "/Undertale.png", false });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Valorant", "/Valorant.png", false });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Name", "ImageURL", "IsInstalled" },
                values: new object[] { "Warframe", "/Warframe.png", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Apex Legends");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Dead Island");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Fortnite");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "LEGO Star Wars");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "LOL");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Naruto");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "NBA 2K26");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "PEAK");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Rocket League");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Subnautica 2");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Tekken 7");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Undertale");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Valorant");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Name",
                keyValue: "Warframe");
        }
    }
}
