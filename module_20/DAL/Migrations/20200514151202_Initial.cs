using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    AverageMark = table.Column<float>(nullable: false, defaultValue: 0),
                    MissedLectures = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ProfessorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lectures_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HomeWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: true),
                    LectureId = table.Column<int>(nullable: true),
                    Presence = table.Column<bool>(nullable: false),
                    Mark = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeWorks_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HomeWorks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Mihail", "Sulimov" },
                    { 2, "Ludmila", "Kozlova" },
                    { 3, "Denis", "Filatov" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AverageMark", "FirstName", "LastName", "MissedLectures" },
                values: new object[,]
                {
                    { 1, 4.67f, "Kirill", "Kononov", 0 },
                    { 2, 0f, "Ivan", "Ivanov", 3 },
                    { 3, 4f, "Semen", "Petrov", 0 },
                    { 4, 1.67f, "Dennis", "Gavrilov", 2 },
                    { 5, 3.33f, "Anton", "Antipov", 0 }
                });

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "Name", "ProfessorId" },
                values: new object[] { 1, "Robotics", 1 });

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "Name", "ProfessorId" },
                values: new object[] { 2, "Mechatronics", 1 });

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "Name", "ProfessorId" },
                values: new object[] { 3, "Physics", 3 });

            migrationBuilder.InsertData(
                table: "HomeWorks",
                columns: new[] { "Id", "Date", "LectureId", "Mark", "Presence", "StudentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, true, 1 },
                    { 2, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, false, 2 },
                    { 3, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, true, 3 },
                    { 4, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, true, 4 },
                    { 5, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, true, 5 },
                    { 6, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5, true, 1 },
                    { 7, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0, false, 2 },
                    { 8, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, true, 3 },
                    { 9, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0, false, 4 },
                    { 10, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5, true, 5 },
                    { 11, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, true, 1 },
                    { 12, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0, false, 2 },
                    { 13, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, true, 3 },
                    { 14, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0, false, 4 },
                    { 15, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0, true, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorks_LectureId",
                table: "HomeWorks",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorks_StudentId",
                table: "HomeWorks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_ProfessorId",
                table: "Lectures",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeWorks");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Professors");
        }
    }
}
