using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessDirectory.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Landlinephone",
                table: "User",
                newName: "LandlinePhone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LandlinePhone",
                table: "User",
                newName: "Landlinephone");
        }
    }
}
