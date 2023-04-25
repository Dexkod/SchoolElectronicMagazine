using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PutTeachersAttitudesToReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsStudents_Reports_ReportId",
                table: "GroupsStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Reports_ReportId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_ReportId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_GroupsStudents_ReportId",
                table: "GroupsStudents");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "GroupsStudents");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Reports",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_TeacherId",
                table: "Reports",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Teachers_TeacherId",
                table: "Reports",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Teachers_TeacherId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_TeacherId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Reports");

            migrationBuilder.AddColumn<Guid>(
                name: "ReportId",
                table: "Teachers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReportId",
                table: "GroupsStudents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ReportId",
                table: "Teachers",
                column: "ReportId",
                unique: true,
                filter: "[ReportId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsStudents_ReportId",
                table: "GroupsStudents",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsStudents_Reports_ReportId",
                table: "GroupsStudents",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Reports_ReportId",
                table: "Teachers",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");
        }
    }
}
