using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PracticeRound1.Migrations
{
    public partial class AddDateModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Department",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Course",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DepartmentCourseCountVM",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CourseCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentCourseCountVM");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Course");
        }
    }
}
