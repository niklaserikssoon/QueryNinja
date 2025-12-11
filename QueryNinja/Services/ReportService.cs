using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; 
using QueryNinja.Data;
using QueryNinja.Models; 

namespace QueryNinja.Services 
{
    public class ReportService
    {


        

        public List<dynamic> GetCourseAverageGrades()
        {
            using (var dbContext = new QueryNinjasDbContext())
            {
                var courseAverages = dbContext.Grades
                    .Include(g => g.Course)
                    .GroupBy(g => g.Course.CourseName)

                    .Select(group => new
                    {
                        CourseName = group.Key,
                        
                        AverageGrade = group.Average(g => Convert.ToDouble(g.GradeValue))
                    })

                    .OrderByDescending(r => r.AverageGrade)
                    .ToList();

                return courseAverages.Cast<dynamic>().ToList();
            }
        }
        

        public List<dynamic> GetNonApprovedStudents()
        {
            using (var dbContext = new QueryNinjasDbContext())
            {
                
                var studentAverages = dbContext.Students
                    .Select(s => new
                    {
                        StudentId = s.StudentID,
                        StudentName = s.FirstName + " " + s.LastName,
                        
                        AvgGrade = s.Grades.Any() ? s.Grades.Average(g => Convert.ToDouble(g.GradeValue)) : 0
                    })
                   
                    .Where(s => s.AvgGrade < 3.0)
                    .OrderByDescending(s => s.AvgGrade)
                    .ToList();

                return studentAverages.Cast<dynamic>().ToList();
            }
        }


        public List<dynamic> GetAllStudentsReport()
        {
            using (var dbContext = new QueryNinjasDbContext())
            {

                var allStudents = dbContext.Students
                    .Select(s => new
                    {
                        ID = s.StudentID,
                        StudentName = s.FirstName + " " + s.LastName,
                        BirthDate = s.BirthDate.ToShortDateString(), 
                        Email = s.Email
                    })
                    .OrderBy(s => s.StudentName)
                    .ToList();

                return allStudents.Cast<dynamic>().ToList();
            }
        }


        public List<dynamic> GetApprovedStudents()
        {
            using (var dbContext = new QueryNinjasDbContext())
            {
                var approvedStudents = dbContext.Students
                    
                    .Select(s => new
                    {
                        StudentId = s.StudentID,
                        StudentName = s.FirstName + " " + s.LastName,
                       
                        AvgGrade = s.Grades.Any() ? s.Grades.Average(g => Convert.ToDouble(g.GradeValue)) : 0
                    })
                    
                    .Where(s => s.AvgGrade >= 3.0)
                    .OrderByDescending(s => s.AvgGrade)
                    .ToList();

                return approvedStudents.Cast<dynamic>().ToList();
            }
        }


        public string GetSimpleReport()
        {
            return "Simple report data.";
        }
    }
}