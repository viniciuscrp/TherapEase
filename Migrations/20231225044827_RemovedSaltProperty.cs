using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TherapEase.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSaltProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Therapists");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Therapists",
                type: "char(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(128)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Therapists",
                type: "char(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(60)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Therapists",
                type: "char(128)",
                nullable: true);
        }
    }
}
