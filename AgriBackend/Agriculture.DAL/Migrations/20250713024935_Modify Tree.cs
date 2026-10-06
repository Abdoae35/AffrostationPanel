using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agriculture.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "TreeNames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TreeNames_TypeId",
                table: "TreeNames",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreeNames_TreeTypes_TypeId",
                table: "TreeNames",
                column: "TypeId",
                principalTable: "TreeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreeNames_TreeTypes_TypeId",
                table: "TreeNames");

            migrationBuilder.DropIndex(
                name: "IX_TreeNames_TypeId",
                table: "TreeNames");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "TreeNames");
        }
    }
}
