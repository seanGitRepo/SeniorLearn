using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearnConsole.Migrations
{
    /// <inheritdoc />
    public partial class ProMemberAdition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Members",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Members",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Member_Type",
                table: "Members",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "proId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProMembermemberId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "proId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProMembermemberId",
                table: "Courses",
                column: "ProMembermemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Members_ProMembermemberId",
                table: "Courses",
                column: "ProMembermemberId",
                principalTable: "Members",
                principalColumn: "memberId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Members_ProMembermemberId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ProMembermemberId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Member_Type",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "proId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ProMembermemberId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "proId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Members",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Members",
                newName: "firstName");
        }
    }
}
