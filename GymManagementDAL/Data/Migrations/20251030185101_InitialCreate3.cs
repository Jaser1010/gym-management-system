using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_memberSessions_Members_MemberId",
                table: "memberSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_memberSessions_Sessions_SessionId",
                table: "memberSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_memberSessions",
                table: "memberSessions");

            migrationBuilder.RenameTable(
                name: "memberSessions",
                newName: "MemberSession");

            migrationBuilder.RenameIndex(
                name: "IX_memberSessions_MemberId",
                table: "MemberSession",
                newName: "IX_MemberSession_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSession",
                table: "MemberSession",
                columns: new[] { "SessionId", "MemberId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSession_Members_MemberId",
                table: "MemberSession",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSession_Sessions_SessionId",
                table: "MemberSession",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSession_Members_MemberId",
                table: "MemberSession");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSession_Sessions_SessionId",
                table: "MemberSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSession",
                table: "MemberSession");

            migrationBuilder.RenameTable(
                name: "MemberSession",
                newName: "memberSessions");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSession_MemberId",
                table: "memberSessions",
                newName: "IX_memberSessions_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_memberSessions",
                table: "memberSessions",
                columns: new[] { "SessionId", "MemberId" });

            migrationBuilder.AddForeignKey(
                name: "FK_memberSessions_Members_MemberId",
                table: "memberSessions",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_memberSessions_Sessions_SessionId",
                table: "memberSessions",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
