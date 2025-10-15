using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSession_Members_MemberId",
                table: "MemberSession");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSession_Sessions_SessionId",
                table: "MemberSession");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShip_Members_MemberId",
                table: "MemberShip");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberShip_Plan_PlanId",
                table: "MemberShip");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Category_CategoryId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plan",
                table: "Plan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberShip",
                table: "MemberShip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSession",
                table: "MemberSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Plan",
                newName: "Plans");

            migrationBuilder.RenameTable(
                name: "MemberShip",
                newName: "memberShips");

            migrationBuilder.RenameTable(
                name: "MemberSession",
                newName: "memberSessions");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_MemberShip_MemberId",
                table: "memberShips",
                newName: "IX_memberShips_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSession_MemberId",
                table: "memberSessions",
                newName: "IX_memberSessions_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plans",
                table: "Plans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_memberShips",
                table: "memberShips",
                columns: new[] { "PlanId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_memberSessions",
                table: "memberSessions",
                columns: new[] { "SessionId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_memberShips_Members_MemberId",
                table: "memberShips",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_memberShips_Plans_PlanId",
                table: "memberShips",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Categories_CategoryId",
                table: "Sessions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_memberSessions_Members_MemberId",
                table: "memberSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_memberSessions_Sessions_SessionId",
                table: "memberSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_memberShips_Members_MemberId",
                table: "memberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_memberShips_Plans_PlanId",
                table: "memberShips");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Categories_CategoryId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plans",
                table: "Plans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_memberShips",
                table: "memberShips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_memberSessions",
                table: "memberSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Plans",
                newName: "Plan");

            migrationBuilder.RenameTable(
                name: "memberShips",
                newName: "MemberShip");

            migrationBuilder.RenameTable(
                name: "memberSessions",
                newName: "MemberSession");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_memberShips_MemberId",
                table: "MemberShip",
                newName: "IX_MemberShip_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_memberSessions_MemberId",
                table: "MemberSession",
                newName: "IX_MemberSession_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plan",
                table: "Plan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberShip",
                table: "MemberShip",
                columns: new[] { "PlanId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSession",
                table: "MemberSession",
                columns: new[] { "SessionId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShip_Members_MemberId",
                table: "MemberShip",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberShip_Plan_PlanId",
                table: "MemberShip",
                column: "PlanId",
                principalTable: "Plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Category_CategoryId",
                table: "Sessions",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
