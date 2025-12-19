using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agriculture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Modifyfklocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationTypeId",
                table: "LocationNames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LocationNames_LocationTypeId",
                table: "LocationNames",
                column: "LocationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationNames_TypeOfLocations_LocationTypeId",
                table: "LocationNames",
                column: "LocationTypeId",
                principalTable: "TypeOfLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationNames_TypeOfLocations_LocationTypeId",
                table: "LocationNames");

            migrationBuilder.DropIndex(
                name: "IX_LocationNames_LocationTypeId",
                table: "LocationNames");

            migrationBuilder.DropColumn(
                name: "LocationTypeId",
                table: "LocationNames");
        }
    }
}
