using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QueryNinja.Migrations
{
    /// <inheritdoc />
    public partial class LisaLocalTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "StoredProcedureResults",
                columns: table => new
                {
                    ResultMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentID", "BirthDate", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.svensson@student.com", "Anna", "Svensson" },
                    { 2, new DateTime(1999, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "erik.johansson@student.com", "Erik", "Johansson" },
                    { 3, new DateTime(2001, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara.lindgren@student.com", "Sara", "Lindgren" },
                    { 4, new DateTime(1998, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.berg@student.com", "David", "Berg" },
                    { 5, new DateTime(2000, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "emma.nystrom@student.com", "Emma", "Nyström" },
                    { 6, new DateTime(1999, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "johan.persson@student.com", "Johan", "Persson" },
                    { 7, new DateTime(2001, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "maja.andersson@student.com", "Maja", "Andersson" },
                    { 8, new DateTime(1998, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "niklas.karlsson@student.com", "Niklas", "Karlsson" },
                    { 9, new DateTime(2000, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "elin.holm@student.com", "Elin", "Holm" },
                    { 10, new DateTime(1999, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "oscar.wikstrom@student.com", "Oscar", "Wikström" },
                    { 11, new DateTime(2001, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "karin.aberg@student.com", "Karin", "Åberg" },
                    { 12, new DateTime(1998, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "mattias.forsberg@student.com", "Mattias", "Forsberg" },
                    { 13, new DateTime(2000, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "linda.strom@student.com", "Linda", "Ström" },
                    { 14, new DateTime(1999, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "patrik.hellgren@student.com", "Patrik", "Hellgren" },
                    { 15, new DateTime(2001, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "sofia.blom@student.com", "Sofia", "Blom" },
                    { 16, new DateTime(1998, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "andreas.lundqvist@student.com", "Andreas", "Lundqvist" },
                    { 17, new DateTime(2000, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "helena.sandberg@student.com", "Helena", "Sandberg" },
                    { 18, new DateTime(1999, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "marcus.viklund@student.com", "Marcus", "Viklund" },
                    { 19, new DateTime(2001, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "ida.norberg@student.com", "Ida", "Norberg" },
                    { 20, new DateTime(1998, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "per.haggstrom@student.com", "Per", "Häggström" }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "GradeId", "FkCourseId", "FkStudentId", "FkTeacherId", "GradeValue" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, "VG" },
                    { 2, 2, 2, 2, "G" },
                    { 3, 3, 3, 3, "IG" },
                    { 4, 4, 4, 1, "VG" },
                    { 9, 9, 9, 1, "G" },
                    { 10, 10, 10, 2, "VG" },
                    { 11, 1, 11, 1, "G" },
                    { 12, 2, 12, 2, "VG" },
                    { 18, 8, 18, 4, "IG" },
                    { 19, 9, 19, 1, "VG" },
                    { 20, 10, 20, 2, "G" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoredProcedureResults");

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 20);

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Registrations");
        }
    }
}
