using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ymUserValidPhoneCheck1",
                table: "Trainers");

            migrationBuilder.DropCheckConstraint(
                name: "ymUserValidPhoneCheck",
                table: "Members");

            migrationBuilder.AddCheckConstraint(
                name: "ymUserValidPhoneCheck1",
                table: "Trainers",
                sql: "Phone Like '01%' and Phone Not Like '%[^0-9]%'");

            migrationBuilder.AddCheckConstraint(
                name: "ymUserValidPhoneCheck",
                table: "Members",
                sql: "Phone Like '01%' and Phone Not Like '%[^0-9]%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ymUserValidPhoneCheck1",
                table: "Trainers");

            migrationBuilder.DropCheckConstraint(
                name: "ymUserValidPhoneCheck",
                table: "Members");

            migrationBuilder.AddCheckConstraint(
                name: "ymUserValidPhoneCheck1",
                table: "Trainers",
                sql: "Phone Like '01%' and Phone Not Like '%[^0:9]%'");

            migrationBuilder.AddCheckConstraint(
                name: "ymUserValidPhoneCheck",
                table: "Members",
                sql: "Phone Like '01%' and Phone Not Like '%[^0:9]%'");
        }
    }
}
