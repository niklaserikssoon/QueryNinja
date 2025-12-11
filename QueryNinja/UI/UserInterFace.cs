using QueryNinja.Data;
using QueryNinja.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryNinja.UI
{
    public class UserInterFace
    {
        // This class will handle all user interface related functionalities
        public void DisplayUI()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Main menu ====");
                Console.WriteLine("1. Course administration");
                Console.WriteLine("2. Student administration");
                Console.WriteLine("3. Schedule administration");
                Console.WriteLine("4. Reports");
                Console.WriteLine("0. Quit");
                Console.Write("Choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        new CourseMenu().ShowCourse();
                        break;
                    case "2":
                        new studentMenu().ShowStudent();
                        break;
                    case "3":
                        new ScheduleMenu().ShowSchedule();
                        break;
                    case "4":
                        new ReportMenu().ShowReport();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ====================================================================
        // 1. COURSE MENU IMPLEMENTATION (Re-implemented for structure)
        // ====================================================================

        public class CourseMenu
        {
            public void ShowCourse()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("==== Course administration ====");
                    Console.WriteLine("1. Create course");
                    Console.WriteLine("2. View courses");
                    Console.WriteLine("3. View active courses");
                    Console.WriteLine("4. Register student on course");
                    Console.WriteLine("0. Back");
                    Console.Write("Choice: ");

                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            CreateCourse();
                            Console.ReadKey();
                            break;
                        case "2":
                            ViewCourses();
                            Console.ReadKey();
                            break;
                        case "3":
                            ViewActiveCourses();
                            Console.ReadKey();
                            break;
                        case "4":
                            RegisterStudentOnCourse();
                            Console.ReadKey();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey();
                            break;
                    }
                }
            }

            // --- Supporting Methods for CourseMenu (Assumed implementation based on previous work) ---
            public void CreateCourse() { Console.WriteLine("Create course logic..."); Console.ReadKey(); }
            public void ViewCourses() { Console.WriteLine("List courses logic..."); Console.ReadKey(); }
            public void ViewActiveCourses() { Console.WriteLine("Active courses logic..."); Console.ReadKey(); }
            public void RegisterStudentOnCourse() { Console.WriteLine("Register student on course logic..."); Console.ReadKey(); }
        }

        // ====================================================================
        // 2. STUDENT MENU IMPLEMENTATION (Re-implemented for structure)
        // ====================================================================

        public class studentMenu
        {
            public void ShowStudent()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("==== Student administration ====");
                    Console.WriteLine("1. Add student");
                    Console.WriteLine("2. Edit student");
                    Console.WriteLine("3. Remove student");
                    Console.WriteLine("4. View all students");
                    Console.WriteLine("5. View student details");
                    Console.WriteLine("0. Back");
                    Console.Write("Choice: ");
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            AddStudent();
                            Console.ReadKey();
                            break;
                        case "2":
                            Console.WriteLine("EditStudent logic...");
                            Console.ReadKey();
                            break;
                        case "3":
                            Console.WriteLine("RemoveStudent logic...");
                            Console.ReadKey();
                            break;
                        case "4":
                            ViewStudents();
                            Console.ReadKey();
                            break;
                        case "5":
                            Console.WriteLine("ViewStudentDetails logic...");
                            Console.ReadKey();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            // --- Supporting Methods for studentMenu (Assumed implementation) ---
            public void ViewStudents() { Console.WriteLine("All students list logic..."); }
            public void AddStudent() { Console.WriteLine("Add student logic..."); }
        }

        // ====================================================================
        // 3. SCHEDULE MENU IMPLEMENTATION (Original structure)
        // ====================================================================

        public class ScheduleMenu
        {
            public void ShowSchedule()
            {
                // ... (Original logic for ScheduleMenu)
                Console.WriteLine("Schedule menu logic...");
                Console.ReadKey();
                return;
            }
        }

        // ====================================================================
        // 4. REPORT MENU IMPLEMENTATION (Unified and Corrected)
        // ====================================================================

        public class ReportMenu
        {
            // Instantiating ReportService for access to LINQ queries
            private readonly Services.ReportService reportService = new Services.ReportService();

            public void ShowReport()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("==== Reports ====");
                    Console.WriteLine("1. Approved students");
                    Console.WriteLine("2. Non-approved students");
                    Console.WriteLine("3. All students");
                    Console.WriteLine("4. Course Average Grades (Complex LINQ Report)");
                    Console.WriteLine("0. Back");
                    Console.Write("Choice: ");

                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            DisplayApprovedStudents();
                            Console.ReadKey();
                            break;
                        case "2":
                            DisplayNonApprovedStudents();
                            Console.ReadKey();
                            break;
                        case "3":
                            DisplayAllStudents();
                            Console.ReadKey();
                            break;
                        case "4":
                            DisplayCourseAverageReport();
                            Console.ReadKey();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey();
                            break;
                    }
                }
            }

            // --- Supporting Display Methods (Calling ReportService) ---

            public void DisplayCourseAverageReport()
            {
                try
                {
                    var results = reportService.GetCourseAverageGrades();

                    if (results.Count == 0)
                    {
                        Console.WriteLine("No results found for the Course Average Grades report.");
                        return;
                    }
                    Console.WriteLine("\n==== Course Average Grades Report ====");
                    foreach (var item in results)
                    {
                        Console.WriteLine($"Course: {item.CourseName,-30} | Average Grade: {item.AverageGrade:F2}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error displaying report: {ex.Message}");
                }
            }

            public void DisplayAllStudents()
            {
                try
                {
                    // Assumed simple report method is GetAllStudentsReport()
                    var results = reportService.GetAllStudentsReport();

                    if (results.Count == 0)
                    {
                        Console.WriteLine("No students found in the database.");
                        return;
                    }
                    Console.WriteLine("\n==== All Registered Students ====");
                    foreach (var item in results)
                    {
                        Console.WriteLine($"ID: {item.ID}, Name: {item.StudentName,-25} | Email: {item.Email}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error displaying report: {ex.Message}");
                }
            }

            public void DisplayApprovedStudents()
            {
                try
                {
                    // Assuming GetApprovedStudents() is implemented in ReportService
                    var results = reportService.GetApprovedStudents();
                    if (results.Count == 0)
                    {
                        Console.WriteLine("No students currently meet the approval criteria (Avg Grade >= 3.0).");
                        return;
                    }
                    Console.WriteLine("\n==== Approved Students Report (Avg Grade >= 3.0) ====");
                    foreach (var item in results)
                    {
                        Console.WriteLine($"ID: {item.StudentId}, Name: {item.StudentName,-25} | Average Grade: {item.AvgGrade:F2}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error displaying report: {ex.Message}");
                }
            }

            public void DisplayNonApprovedStudents()
            {
                try
                {
                    // Assuming GetNonApprovedStudents() is implemented in ReportService
                    var results = reportService.GetNonApprovedStudents();
                    if (results.Count == 0)
                    {
                        Console.WriteLine("All students meet the approval criteria or no data found.");
                        return;
                    }
                    Console.WriteLine("\n==== Non-Approved Students Report (Avg Grade < 3.0) ====");
                    foreach (var item in results)
                    {
                        Console.WriteLine($"ID: {item.StudentId}, Name: {item.StudentName,-25} | Average Grade: {item.AvgGrade:F2}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error displaying report: {ex.Message}");
                }
            }
        }
    }
}