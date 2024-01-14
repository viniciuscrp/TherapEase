using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TherapEase.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmailConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "UserRefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Therapists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Pacients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Pacients");
        }
    }
}
