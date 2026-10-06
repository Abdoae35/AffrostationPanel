using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agriculture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ModifySechdule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TypeOfLocations",
                newName: "LocationType");

            migrationBuilder.AddColumn<int>(
                name: "LocationTypeId",
                table: "AfforestationAgricultureAchievements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AfforestationAgricultureAchievements_LocationTypeId",
                table: "AfforestationAgricultureAchievements",
                column: "LocationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AfforestationAgricultureAchievements_TypeOfLocations_LocationTypeId",
                table: "AfforestationAgricultureAchievements",
                column: "LocationTypeId",
                principalTable: "TypeOfLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AfforestationAgricultureAchievements_TypeOfLocations_LocationTypeId",
                table: "AfforestationAgricultureAchievements");

            migrationBuilder.DropIndex(
                name: "IX_AfforestationAgricultureAchievements_LocationTypeId",
                table: "AfforestationAgricultureAchievements");

            migrationBuilder.DropColumn(
                name: "LocationTypeId",
                table: "AfforestationAgricultureAchievements");

            migrationBuilder.RenameColumn(
                name: "LocationType",
                table: "TypeOfLocations",
                newName: "Name");
        }
    }
}
