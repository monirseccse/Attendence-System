using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentNo3.Migrations
{
    public partial class AddCourseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Courses",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Courses");
        }
    }
}
