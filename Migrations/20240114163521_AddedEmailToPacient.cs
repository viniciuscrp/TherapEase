using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TherapEase.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmailToPacient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Pacients",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacients_Email",
                table: "Pacients",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pacients_Email",
                table: "Pacients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Pacients");
        }
    }
}
