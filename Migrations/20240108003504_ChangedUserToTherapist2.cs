using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TherapEase.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUserToTherapist2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TherapistId",
                table: "Pacients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pacients_TherapistId",
                table: "Pacients",
                column: "TherapistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacients_Therapists_TherapistId",
                table: "Pacients",
                column: "TherapistId",
                principalTable: "Therapists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacients_Therapists_TherapistId",
                table: "Pacients");

            migrationBuilder.DropIndex(
                name: "IX_Pacients_TherapistId",
                table: "Pacients");

            migrationBuilder.DropColumn(
                name: "TherapistId",
                table: "Pacients");
        }
    }
}
