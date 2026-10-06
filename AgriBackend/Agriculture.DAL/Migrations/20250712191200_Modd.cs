using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agriculture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Modd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TreeNameId",
                table: "AfforestationAgricultureAchievements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AfforestationAgricultureAchievements_TreeNameId",
                table: "AfforestationAgricultureAchievements",
                column: "TreeNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AfforestationAgricultureAchievements_TreeNames_TreeNameId",
                table: "AfforestationAgricultureAchievements",
                column: "TreeNameId",
                principalTable: "TreeNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AfforestationAgricultureAchievements_TreeNames_TreeNameId",
                table: "AfforestationAgricultureAchievements");

            migrationBuilder.DropIndex(
                name: "IX_AfforestationAgricultureAchievements_TreeNameId",
                table: "AfforestationAgricultureAchievements");

            migrationBuilder.DropColumn(
                name: "TreeNameId",
                table: "AfforestationAgricultureAchievements");
        }
    }
}
