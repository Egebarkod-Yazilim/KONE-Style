using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KONE.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class newInitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_CurrentCardAddressMapping_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping",
                column: "CurrentCardLandNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping",
                column: "CurrentCardLandNameId",
                principalTable: "CurrentCardLandName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentCardAddressMapping_CurrentCardLandName_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping");

            migrationBuilder.DropIndex(
                name: "IX_CurrentCardAddressMapping_CurrentCardLandNameId",
                table: "CurrentCardAddressMapping");

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
    }
}
