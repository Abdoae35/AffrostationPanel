using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agriculture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class OnetoManyUserandAffro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AfforestationAgricultureAchievements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AfforestationAgricultureAchievements_UserId",
                table: "AfforestationAgricultureAchievements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AfforestationAgricultureAchievements_Users_UserId",
                table: "AfforestationAgricultureAchievements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AfforestationAgricultureAchievements_Users_UserId",
                table: "AfforestationAgricultureAchievements");

            migrationBuilder.DropIndex(
                name: "IX_AfforestationAgricultureAchievements_UserId",
                table: "AfforestationAgricultureAchievements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AfforestationAgricultureAchievements");
        }
    }
}
