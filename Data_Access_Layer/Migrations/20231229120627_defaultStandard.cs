using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class defaultStandard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Users",
                newName: "UserRole");

            migrationBuilder.RenameColumn(
                name: "SongName",
                table: "Songs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "SongGenre",
                table: "Songs",
                newName: "Genre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "Users",
                newName: "UserType");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Songs",
                newName: "SongName");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Songs",
                newName: "SongGenre");
        }
    }
}
