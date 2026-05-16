using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroTripProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Destinations_DestinationId",
                table: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_Destinations_DestinationId",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Destinations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Destinations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_DestinationId",
                table: "Destinations",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Destinations_DestinationId",
                table: "Destinations",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id");
        }
    }
}
