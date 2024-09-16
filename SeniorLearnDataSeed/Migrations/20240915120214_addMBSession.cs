using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniorLearnDataSeed.Migrations
{
    /// <inheritdoc />
    public partial class addMBSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Sessions");

            migrationBuilder.AddColumn<string>(
                name: "session_type",
                table: "Sessions",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2024, 9, 15, 22, 2, 14, 70, DateTimeKind.Local).AddTicks(626));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2024, 9, 15, 22, 2, 14, 70, DateTimeKind.Local).AddTicks(664));

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "AdditionalDetails", "CourseId", "Date", "MeetingCode", "OnlineLink", "Status", "session_type" },
                values: new object[] { 1, "Cameras are required", 1, new DateTime(2024, 9, 15, 22, 2, 14, 70, DateTimeKind.Local).AddTicks(826), null, "microsoft.com", 1, "session_online" });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "CourseId", "Date", "Status", "StreetName", "StreetNumber", "Suburb", "session_type" },
                values: new object[] { 2, 2, new DateTime(2024, 9, 15, 22, 2, 14, 70, DateTimeKind.Local).AddTicks(845), 1, "Pitt Street", "25A", "Sydney", "session_onprem" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "session_type",
                table: "Sessions");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Sessions",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2024, 9, 15, 14, 15, 38, 71, DateTimeKind.Local).AddTicks(9212));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2,
                column: "StartDate",
                value: new DateTime(2024, 9, 15, 14, 15, 38, 71, DateTimeKind.Local).AddTicks(9254));

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "AdditionalDetails", "CourseId", "Date", "Discriminator", "MeetingCode", "OnlineLink", "Status" },
                values: new object[] { 1, "Cameras are required", 1, new DateTime(2024, 9, 15, 14, 15, 38, 73, DateTimeKind.Local).AddTicks(1461), "OnlineSession", null, "microsoft.com", 1 });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "CourseId", "Date", "Discriminator", "Status", "StreetName", "StreetNumber", "Suburb" },
                values: new object[] { 2, 2, new DateTime(2024, 9, 15, 14, 15, 38, 74, DateTimeKind.Local).AddTicks(1610), "OnPremSession", 1, "Pitt Street", "25A", "Sydney" });
        }
    }
}
