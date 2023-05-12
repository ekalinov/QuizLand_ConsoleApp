using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.Data.Migrations
{
    public partial class UserQuizzes_ElapsedTimeAndUserPointsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "UserElapsedTime",
                table: "UsersQuizzes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "UserPoints",
                table: "UsersQuizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserElapsedTime",
                table: "UsersQuizzes");

            migrationBuilder.DropColumn(
                name: "UserPoints",
                table: "UsersQuizzes");
        }
    }
}
