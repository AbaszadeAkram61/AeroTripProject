using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AeroTripProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fix_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TransferMoneys",
                newName: "StatusString");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "TransferMoneys",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Reservations",
                newName: "StatusString");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "SubAbouts",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "NewsLetters",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "About2s",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TransferMoneys");

            migrationBuilder.RenameColumn(
                name: "StatusString",
                table: "TransferMoneys",
                newName: "Status");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "StatusString",
                table: "Reservations",
                newName: "Status");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubAbouts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NewsLetters");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "About2s");
        }
    }
}
