using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoModelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInstaled",
                table: "Games",
                newName: "IsInstalled");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInstalled",
                table: "Games",
                newName: "IsInstaled");
        }
    }
}
