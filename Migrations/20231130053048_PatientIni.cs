using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterMomshWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class PatientIni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyBook_user_credential_user_Id",
                table: "BabyBook");

            migrationBuilder.RenameColumn(
                name: "user_Id",
                table: "BabyBook",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_BabyBook_user_Id",
                table: "BabyBook",
                newName: "IX_BabyBook_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyBook_user_credential_user_id",
                table: "BabyBook",
                column: "user_id",
                principalTable: "user_credential",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BabyBook_user_credential_user_id",
                table: "BabyBook");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "BabyBook",
                newName: "user_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BabyBook_user_id",
                table: "BabyBook",
                newName: "IX_BabyBook_user_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BabyBook_user_credential_user_Id",
                table: "BabyBook",
                column: "user_Id",
                principalTable: "user_credential",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
