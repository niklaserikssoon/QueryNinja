using QueryNinja.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using QueryNinja.Models;

namespace QueryNinja.Service
{
    public class ReportService
    {
        private readonly QueryNinjasDbContext _context;

        public ReportService(QueryNinjasDbContext context)
        {
            _context = context;
        }

        public IEnumerable<object> GetActiveCoursesReport()
        {
            var today = DateTime.Today;

            var activeCourses = _context.Courses
                .Where(c => c.StartDate <= today && c.EndDate >= today)
                .Select(c => new
                {
                    c.CourseId,
                    c.CourseName,
                    c.StartDate,
                    c.EndDate
                })
                .ToList();

            return activeCourses;
        }

        public IEnumerable<object> GetStudentOverviewReport()
        {

            var overview = _context.Students
                .Select(s => new {
                    s.StudentID,
                    s.FirstName,
                    s.LastName,
                    TotalCourses = _context.Registrations.Count(r => r.FkStudentId == s.StudentID)
                })
                .ToList();

            return overview;
        }

        public string CallRegisterStudentSP(int studentId, int courseId)
        {
            try
            {
                var studentIdParam = new Microsoft.Data.SqlClient.SqlParameter("@StudentId", studentId);
                var courseIdParam = new Microsoft.Data.SqlClient.SqlParameter("@CourseId", courseId);

                var result = _context.StoredProcedureResults
                    .FromSqlRaw("EXEC RegisterStudentForCourse @StudentId, @CourseId", studentIdParam, courseIdParam)
                    .AsEnumerable()
                    .FirstOrDefault();

                return result?.ResultMessage ?? "Registration completed successfully, but no message was returned.";
            }
            catch (Exception ex)
            {
                return $"Error executing stored procedure: {ex.Message}";
            }
        }
    }
}