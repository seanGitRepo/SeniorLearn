using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearnConsole.Migrations
{
    /// <inheritdoc />
    public partial class CourseID3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "courseId",
                table: "MemberSession",
                newName: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "MemberSession",
                newName: "courseId");
        }
    }
}
