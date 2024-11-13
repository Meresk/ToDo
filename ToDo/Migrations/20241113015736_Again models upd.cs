using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.Migrations
{
    /// <inheritdoc />
    public partial class Againmodelsupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_lw9_02_Users_lw9_02_User_lw9_02Id",
                table: "Notes_lw9_02");

            migrationBuilder.DropIndex(
                name: "IX_Notes_lw9_02_User_lw9_02Id",
                table: "Notes_lw9_02");

            migrationBuilder.DropColumn(
                name: "User_lw9_02Id",
                table: "Notes_lw9_02");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_lw9_02_UserId",
                table: "Notes_lw9_02",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_lw9_02_Users_lw9_02_UserId",
                table: "Notes_lw9_02",
                column: "UserId",
                principalTable: "Users_lw9_02",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_lw9_02_Users_lw9_02_UserId",
                table: "Notes_lw9_02");

            migrationBuilder.DropIndex(
                name: "IX_Notes_lw9_02_UserId",
                table: "Notes_lw9_02");

            migrationBuilder.AddColumn<int>(
                name: "User_lw9_02Id",
                table: "Notes_lw9_02",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_lw9_02_User_lw9_02Id",
                table: "Notes_lw9_02",
                column: "User_lw9_02Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_lw9_02_Users_lw9_02_User_lw9_02Id",
                table: "Notes_lw9_02",
                column: "User_lw9_02Id",
                principalTable: "Users_lw9_02",
                principalColumn: "Id");
        }
    }
}
