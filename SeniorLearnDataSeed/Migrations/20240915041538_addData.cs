using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SeniorLearnDataSeed.Migrations
{
    /// <inheritdoc />
    public partial class addData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalDetails",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Sessions",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MeetingCode",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnlineLink",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetName",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetNumber",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suburb",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "Email", "FirstName", "LastName", "StartDate", "Type" },
                values: new object[,]
                {
                    { 1, "saxon.cadet@gelos.com", "Saxon", "Cadet", new DateTime(2024, 9, 15, 14, 15, 38, 71, DateTimeKind.Local).AddTicks(9212), 0 },
                    { 2, "sean.saap@gelos.com", "Sean", "Saap", new DateTime(2024, 9, 15, 14, 15, 38, 71, DateTimeKind.Local).AddTicks(9254), 1 }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "CourseId", "Date", "Discriminator", "Status", "StreetName", "StreetNumber", "Suburb" },
                values: new object[] { 2, 2, new DateTime(2024, 9, 15, 14, 15, 38, 74, DateTimeKind.Local).AddTicks(1610), "OnPremSession", 1, "Pitt Street", "25A", "Sydney" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Description", "MemberId", "Name", "isStandAlone" },
                values: new object[] { 1, "Beginner's guide to gardening", 1, "Gardening", true });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "MemberId", "SessionId" },
                values: new object[] { 2, 2, 2 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "AmountPaid", "MemberId", "PaymentType" },
                values: new object[,]
                {
                    { 1, 100.0, 1, 0 },
                    { 2, 150.0, 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "SessionId", "AdditionalDetails", "CourseId", "Date", "Discriminator", "MeetingCode", "OnlineLink", "Status" },
                values: new object[] { 1, "Cameras are required", 1, new DateTime(2024, 9, 15, 14, 15, 38, 73, DateTimeKind.Local).AddTicks(1461), "OnlineSession", null, "microsoft.com", 1 });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "MemberId", "SessionId" },
                values: new object[] { 1, 2, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sessions",
                keyColumn: "SessionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "AdditionalDetails",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "MeetingCode",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "OnlineLink",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StreetName",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Suburb",
                table: "Sessions");
        }
    }
}
