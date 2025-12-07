using QueryNinja.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryNinja.Service;
using QueryNinja.Data;

namespace QueryNinja.UI
{
    public class UserInterFace
    {
        private readonly ReportService _reportService;

        public UserInterFace(ReportService reportService)
        {
            _reportService = reportService;
        }

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
                        new ReportMenu(_reportService).ShowReport();
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
                            Console.WriteLine("Create course not implemented yet.");
                            Console.ReadKey();
                            break;
                        case "2":
                            Console.WriteLine("List courses not implemented.");
                            Console.ReadKey();
                            break;
                        case "3":
                            Console.WriteLine("Active courses not implemented.");
                            Console.ReadKey();
                            break;
                        case "4":
                            Console.WriteLine("Register student on course not implemented.");
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
        }

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
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case "3":
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case "4":
                            ViewStudents();
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case "5":
                            Console.WriteLine("Press any key to continue...");
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
            public void ViewStudents()
            {
                var dbContext = new QueryNinjasDbContext();
                var students = dbContext.Students.ToList();
                Console.WriteLine("==== Students List ====");
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, Birth Date: {student.BirthDate.ToShortDateString()}, Email: {student.Email}");
                }
            }

            public void AddStudent()
            {
                Console.Write("Enter student first name: ");
                var firstName = Console.ReadLine();
                Console.Write("Enter student last name: ");
                var lastName = Console.ReadLine();
                Console.Write("Enter student birth date (yyyy-mm-dd): ");
                var birthDateInput = Console.ReadLine();
                DateTime birthDate;
                if (!DateTime.TryParse(birthDateInput, out birthDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }
                Console.Write("Enter student email: ");
                var email = Console.ReadLine();
                var dbContext = new QueryNinjasDbContext();
                var student = new Models.Student
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = birthDate,
                    Email = email
                };
                dbContext.Students.Add(student);
                dbContext.SaveChanges();
                Console.WriteLine("Student added successfully.");
            }
        }

        public class ScheduleMenu
        {
            public void ShowSchedule()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("==== Schedule administration ====");
                    Console.WriteLine("1. View schedule");
                    Console.WriteLine("2. Add schedule item");
                    Console.WriteLine("3. Manage teachers");
                    Console.WriteLine("4. Manage classrooms");
                    Console.WriteLine("0. Back");
                    Console.Write("Choice: ");
                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("View schedule not implemented yet.");
                            Console.ReadKey();
                            break;
                        case "2":
                            Console.WriteLine("Add scheduele item not implemented yet.");
                            Console.ReadKey();
                            break;
                        case "3":
                            Console.WriteLine("Manage teachers not implemented yet.");
                            Console.ReadKey();
                            break;
                        case "4":
                            Console.WriteLine("Manage classrooms not implemented yet.");
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
        }

        public class ReportMenu
        {
            private readonly ReportService _reportService;

            public ReportMenu(ReportService reportService)
            {
                _reportService = reportService;
            }

            public void ShowReport()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("==== Reports ====");
                    Console.WriteLine("1. Active Courses Report");
                    Console.WriteLine("2. Student Overview Report");
                    Console.WriteLine("3. Register Student (Stored Procedure)");
                    Console.WriteLine("0. Back");
                    Console.Write("Choice: ");
                    var input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            DisplayActiveCourses();
                            Console.ReadKey();
                            break;
                        case "2":
                            DisplayStudentOverview();
                            Console.ReadKey();
                            break;
                        case "3":
                            RegisterStudentViaSP();
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

            private void DisplayActiveCourses()
            {
                Console.Clear();
                Console.WriteLine("--- Active Courses ---");
                var courses = _reportService.GetActiveCoursesReport();

                foreach (dynamic course in courses)
                {
                    Console.WriteLine($"ID: {course.CourseId}, Name: {course.CourseName}, End Date: {course.EndDate.ToShortDateString()}");
                }
            }

            private void DisplayStudentOverview()
            {
                Console.Clear();
                Console.WriteLine("--- Student Overview ---");
                var overview = _reportService.GetStudentOverviewReport();

                foreach (dynamic student in overview)
                {
                    Console.WriteLine($"ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, Courses Enrolled: {student.TotalCourses}");
                }
            }

            private void RegisterStudentViaSP()
            {
                Console.Clear();
                Console.WriteLine("--- Register Student via Stored Procedure ---");
                Console.Write("Student ID: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId)) return;

                Console.Write("Course ID: ");
                if (!int.TryParse(Console.ReadLine(), out int courseId)) return;

                var result = _reportService.CallRegisterStudentSP(studentId, courseId);
                Console.WriteLine($"SP Result: {result}");
            }
        }
    }
}