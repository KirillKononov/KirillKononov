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
                    AverageMark = table.Column<float>(nullable: false),
                    MissedLectures = table.Column<int>(nullable: false)
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
                name: "Homework",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: true),
                    LectureId = table.Column<int>(nullable: true),
                    StudentPresence = table.Column<bool>(nullable: false),
                    HomeworkPresence = table.Column<bool>(nullable: false),
                    Mark = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homework", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homework_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homework_Students_StudentId",
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
                    { 5, 3.67f, "Anton", "Antipov", 0 }
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
                table: "Homework",
                columns: new[] { "Id", "Date", "HomeworkPresence", "LectureId", "Mark", "StudentId", "StudentPresence" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 5, 1, true },
                    { 2, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 0, 2, false },
                    { 3, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 4, 3, true },
                    { 4, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 5, 4, true },
                    { 5, new DateTime(2019, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 5, 5, true },
                    { 6, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 5, 1, true },
                    { 7, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 0, 2, false },
                    { 8, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 4, 3, true },
                    { 9, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 0, 4, false },
                    { 10, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 5, 5, true },
                    { 11, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, 4, 1, true },
                    { 12, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 0, 2, false },
                    { 13, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, 4, 3, true },
                    { 14, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 0, 4, false },
                    { 15, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 3, 1, 5, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Homework_LectureId",
                table: "Homework",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_StudentId",
                table: "Homework",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_ProfessorId",
                table: "Lectures",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Homework");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Professors");
        }
    }
}
