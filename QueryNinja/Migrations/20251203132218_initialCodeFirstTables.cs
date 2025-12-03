using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QueryNinja.Migrations
{
    /// <inheritdoc />
    public partial class initialCodeFirstTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassRooms",
                columns: table => new
                {
                    ClassRoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRooms", x => x.ClassRoomId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FkTeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_FkTeacherId",
                        column: x => x.FkTeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkCourseId = table.Column<int>(type: "int", nullable: false),
                    FkClassRoomId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_ClassRooms_FkClassRoomId",
                        column: x => x.FkClassRoomId,
                        principalTable: "ClassRooms",
                        principalColumn: "ClassRoomId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Courses_FkCourseId",
                        column: x => x.FkCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkTeacherId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    FkCourseId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    FkStudentId = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                    table.ForeignKey(
                        name: "FK_Grades_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkStudentId = table.Column<int>(type: "int", nullable: false),
                    FkCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_Registrations_Courses_FkCourseId",
                        column: x => x.FkCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrations_Student_FkStudentId",
                        column: x => x.FkStudentId,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ClassRooms",
                columns: new[] { "ClassRoomId", "RoomNumber" },
                values: new object[,]
                {
                    { 1, 101 },
                    { 2, 102 },
                    { 3, 103 },
                    { 4, 104 },
                    { 5, 105 },
                    { 6, 106 },
                    { 7, 107 },
                    { 8, 108 },
                    { 9, 109 },
                    { 10, 110 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherId", "AreaOfExpertise", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Backend Development", "karin.lund@school.com", "Karin", "Lund" },
                    { 2, "Databases", "peter.sund@school.com", "Peter", "Sund" },
                    { 3, "Frontend Development", "maria.ekstrom@school.com", "Maria", "Ekström" },
                    { 4, "DevOps", "jonas.bjork@school.com", "Jonas", "Björk" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseName", "EndDate", "FkTeacherId", "StartDate" },
                values: new object[,]
                {
                    { 1, "C# Programming", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Database Design", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Web Development", new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Software Architecture", new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Agile Methods", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "JavaScript Basics", new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Cloud Computing", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "DevOps", new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "UML & Modeling", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "Security Fundamentals", new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleId", "EndTime", "FkClassRoomId", "FkCourseId", "StartTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2025, 1, 15, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 1, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, new DateTime(2025, 1, 15, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 8, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, new DateTime(2025, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2025, 8, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, new DateTime(2025, 8, 20, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 5, 5, new DateTime(2025, 1, 15, 8, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 8, 20, 13, 0, 0, 0, DateTimeKind.Unspecified), 6, 6, new DateTime(2025, 8, 20, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2025, 1, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), 7, 7, new DateTime(2025, 1, 15, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2025, 8, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), 8, 8, new DateTime(2025, 8, 20, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2025, 1, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), 9, 9, new DateTime(2025, 1, 15, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2025, 8, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 10, 10, new DateTime(2025, 8, 20, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassRooms_RoomNumber",
                table: "ClassRooms",
                column: "RoomNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseName",
                table: "Courses",
                column: "CourseName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FkTeacherId",
                table: "Courses",
                column: "FkTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseId",
                table: "Grades",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentID",
                table: "Grades",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TeacherId",
                table: "Grades",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_FkCourseId",
                table: "Registrations",
                column: "FkCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_FkStudentId",
                table: "Registrations",
                column: "FkStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FkClassRoomId",
                table: "Schedules",
                column: "FkClassRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_FkCourseId",
                table: "Schedules",
                column: "FkCourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "ClassRooms");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "TeacherId",
                keyValue: 4);
        }
    }
}
