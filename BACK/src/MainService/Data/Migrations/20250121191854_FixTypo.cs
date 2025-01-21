using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainService.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Treshold",
                table: "UserItems",
                newName: "Threshold");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Threshold",
                table: "UserItems",
                newName: "Treshold");
        }
    }
}
