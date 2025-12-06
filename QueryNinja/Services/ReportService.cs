using QueryNinja.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    c.CourseID,
                    c.Title,
                    c.StartDate,
                    c.EndDate
                })
                .ToList();

            return activeCourses;
        }


        public object GetStudentOverviewReport()
        {
           
            var overview = _context.Students
                .Select(s => new {
                    s.StudentID,
                    s.FirstName,
                    TotalCourses = _context.Registrations.Count(r => r.StudentID == s.StudentID)
                })
                .ToList();

            return overview;
        }

        internal string CallRegisterStudentSP(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }
    }
}

