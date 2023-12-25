using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TherapEase.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmailColumnUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Therapists_Email",
                table: "Therapists",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Therapists_Email",
                table: "Therapists");
        }
    }
}
