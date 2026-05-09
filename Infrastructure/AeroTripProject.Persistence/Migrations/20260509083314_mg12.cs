using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroTripProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservastion_AspNetUsers_AppUserId",
                table: "Reservastion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservastion",
                table: "Reservastion");

            migrationBuilder.RenameTable(
                name: "Reservastion",
                newName: "Reservastions");

            migrationBuilder.RenameIndex(
                name: "IX_Reservastion_AppUserId",
                table: "Reservastions",
                newName: "IX_Reservastions_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservastions",
                table: "Reservastions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservastions_AspNetUsers_AppUserId",
                table: "Reservastions",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservastions_AspNetUsers_AppUserId",
                table: "Reservastions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservastions",
                table: "Reservastions");

            migrationBuilder.RenameTable(
                name: "Reservastions",
                newName: "Reservastion");

            migrationBuilder.RenameIndex(
                name: "IX_Reservastions_AppUserId",
                table: "Reservastion",
                newName: "IX_Reservastion_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservastion",
                table: "Reservastion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservastion_AspNetUsers_AppUserId",
                table: "Reservastion",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
