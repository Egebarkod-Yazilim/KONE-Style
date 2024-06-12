using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KONE.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class newInitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_LandNameId",
                table: "CurrentCardAddressMapping");

            migrationBuilder.DropIndex(
                name: "IX_CurrentCardAddressMapping_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping");

            migrationBuilder.DropIndex(
                name: "IX_CurrentCardAddressMapping_LandNameId",
                table: "CurrentCardAddressMapping");

            migrationBuilder.DropColumn(
                name: "LandNameId",
                table: "CurrentCardAddressMapping");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentCardLandNameId",
                table: "CurrentCardAddressMapping",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentCardLandNameId1",
                table: "CurrentCardAddressMapping",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCardAddressMapping_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping",
                column: "CurrentCardLandNameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCardAddressMapping_CurrentCardLandNameId1",
                table: "CurrentCardAddressMapping",
                column: "CurrentCardLandNameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping",
                column: "CurrentCardLandNameId",
                principalTable: "CurrentCardLandName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_CurrentCardLandNameId1",
                table: "CurrentCardAddressMapping",
                column: "CurrentCardLandNameId1",
                principalTable: "CurrentCardLandName",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_CurrentCardLandNameId1",
                table: "CurrentCardAddressMapping");

            migrationBuilder.DropIndex(
                name: "IX_CurrentCardAddressMapping_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping");

            migrationBuilder.DropIndex(
                name: "IX_CurrentCardAddressMapping_CurrentCardLandNameId1",
                table: "CurrentCardAddressMapping");

            migrationBuilder.DropColumn(
                name: "CurrentCardLandNameId1",
                table: "CurrentCardAddressMapping");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentCardLandNameId",
                table: "CurrentCardAddressMapping",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LandNameId",
                table: "CurrentCardAddressMapping",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCardAddressMapping_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping",
                column: "CurrentCardLandNameId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCardAddressMapping_LandNameId",
                table: "CurrentCardAddressMapping",
                column: "LandNameId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping",
                column: "CurrentCardLandNameId",
                principalTable: "CurrentCardLandName",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_LandNameId",
                table: "CurrentCardAddressMapping",
                column: "LandNameId",
                principalTable: "CurrentCardLandName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
