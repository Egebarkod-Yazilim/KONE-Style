using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KONE.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changedCoordinateType4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMultiPolygon",
                table: "Coordinates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Part",
                table: "Coordinates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMultiPolygon",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "Part",
                table: "Coordinates");
        }
    }
}
