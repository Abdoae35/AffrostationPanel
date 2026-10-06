using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agriculture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class modi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "AfforestationAgricultureAchievements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "AfforestationAgricultureAchievements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
