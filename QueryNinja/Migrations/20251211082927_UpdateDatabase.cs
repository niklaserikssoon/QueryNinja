using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QueryNinja.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 10,
                columns: new[] { "FkCourseId", "FkStudentId", "FkTeacherId" },
                values: new object[] { 9, 19, 1 });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "GradeId", "FkCourseId", "FkStudentId", "FkTeacherId", "GradeValue" },
                values: new object[,]
                {
                    { 5, 9, 9, 1, "G" },
                    { 7, 1, 11, 1, "G" },
                    { 8, 2, 12, 2, "VG" }
                });

            migrationBuilder.InsertData(
                table: "Registrations",
                columns: new[] { "RegistrationId", "FkCourseId", "FkStudentId", "RegistrationDate" },
                values: new object[,]
                {
                    { 7, 3, 3, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 4, 4, new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 10, 10, new DateTime(2025, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 8, 18, new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 10, 20, new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 3, 5, new DateTime(2025, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 4, 6, new DateTime(2025, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, 6, 7, new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, 8, 8, new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 10, 13, new DateTime(2025, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, 3, 14, new DateTime(2025, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, 4, 15, new DateTime(2025, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, 6, 16, new DateTime(2025, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, 8, 17, new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Registrations",
                keyColumn: "RegistrationId",
                keyValue: 29);

            migrationBuilder.UpdateData(
                table: "Grades",
                keyColumn: "GradeId",
                keyValue: 10,
                columns: new[] { "FkCourseId", "FkStudentId", "FkTeacherId" },
                values: new object[] { 10, 10, 2 });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "GradeId", "FkCourseId", "FkStudentId", "FkTeacherId", "GradeValue" },
                values: new object[,]
                {
                    { 3, 3, 3, 3, "IG" },
                    { 4, 4, 4, 1, "VG" },
                    { 9, 9, 9, 1, "G" },
                    { 11, 1, 11, 1, "G" },
                    { 12, 2, 12, 2, "VG" },
                    { 18, 8, 18, 4, "IG" },
                    { 19, 9, 19, 1, "VG" },
                    { 20, 10, 20, 2, "G" }
                });
        }
    }
}
