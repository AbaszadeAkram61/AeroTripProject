using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroTripProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Destinations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DestinationId",
                table: "Reservations",
                column: "DestinationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Destinations_DestinationId",
                table: "Reservations",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Destinations_DestinationId",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Destinations_DestinationId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_DestinationId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Destinations_DestinationId",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Destinations");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
