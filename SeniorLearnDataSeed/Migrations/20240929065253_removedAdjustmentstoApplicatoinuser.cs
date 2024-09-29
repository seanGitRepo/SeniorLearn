using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearnDataSeed.Migrations
{
    /// <inheritdoc />
    public partial class removedAdjustmentstoApplicatoinuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userType",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userType",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
