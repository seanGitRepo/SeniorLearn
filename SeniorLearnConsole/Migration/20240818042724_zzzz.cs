using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearnConsole.Migrations
{
    /// <inheritdoc />
    public partial class zzzz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSession_Courses_memberId",
                table: "MemberSession");

            migrationBuilder.CreateIndex(
                name: "IX_MemberSession_CourseId",
                table: "MemberSession",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSession_Courses_CourseId",
                table: "MemberSession",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSession_Courses_CourseId",
                table: "MemberSession");

            migrationBuilder.DropIndex(
                name: "IX_MemberSession_CourseId",
                table: "MemberSession");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSession_Courses_memberId",
                table: "MemberSession",
                column: "memberId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
