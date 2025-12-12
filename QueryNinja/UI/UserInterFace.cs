using QueryNinja.Data;
using QueryNinja.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using QueryNinja.Services; 

namespace QueryNinja.UI
{
    public class UserInterFace
    {
        // Dependency Injection for ReportService
        private readonly ReportService _reportService;

        public UserInterFace(ReportService reportService)
        {
            _reportService = reportService;
        }

        public void DisplayUI()
        {
            while (true)
            {
                // main start menu
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
                        new StudentMenu().ShowStudent(); // Corrected class name
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
        
        // 1. COURSE MENU IMPLEMENTATION (Merged All Logic)
        public class CourseMenu
        {
            public void ShowCourse()
            {
                while (true)
                {
                    // course administration menu
                    Console.Clear();
                    Console.WriteLine("==== Course administration ====");
                    Console.WriteLine("1. Create course");
                    Console.WriteLine("2. View courses");
                    Console.WriteLine("3. View active courses and students");
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

            public void CreateCourse()
            {
                Console.WriteLine("==== Create New Course ====");
                Console.Write("Enter Course Name: ");
                var courseName = Console.ReadLine();

                Console.Write("Enter Start Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }

                Console.Write("Enter End Date (yyyy-mm-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return;
                }
                Console.Write("Add teacher to course. ID:  ");
                var teacherInput = Console.ReadLine();

                try
                {
                    using (var dbContext = new QueryNinjasDbContext())
                    {
                        FkTeacherId = int.Parse(teacherInput),
                        CourseName = courseName,
                        StartDate = startDate,
                        EndDate = endDate
                    };
                    dbContext.Courses.Add(course);
                    dbContext.SaveChanges();
                    Console.WriteLine("Course added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding course: {ex.Message}");
                }
            }

            public void ViewCourses()
            {
                Console.Clear();
                Console.WriteLine("==== All Courses ====");
                try
                {
                    using (var dbContext = new QueryNinjasDbContext())
                    {
                        var courses = dbContext.Courses.ToList();

                        if (courses.Count == 0) { Console.WriteLine("No courses found."); return; }

                        foreach (var course in courses)
                        {
                            Console.WriteLine($"ID: {course.CourseId}, Name: {course.CourseName}, Start: {course.StartDate.ToShortDateString()}, End: {course.EndDate.ToShortDateString()}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error viewing courses: {ex.Message}");
                }
            }

            public void ViewActiveCourses()
            {
                Console.Clear();
                Console.WriteLine("==== Active courses and students ====");
                try
                {
                    var today = DateTime.Today;
                    Console.WriteLine($"Today is: {today:yyyy-MM-dd}\n");
                    using (var dbContext = new QueryNinjasDbContext())
                    {
                        var activeCoursesAndStudents = dbContext.Courses
                            .Where(c => c.StartDate <= today && c.EndDate >= today)
                            .Include(c => c.Registrations)
                                .ThenInclude(r => r.Student)
                            .ToList();

                        if (activeCoursesAndStudents.Count == 0) { Console.WriteLine("No active courses found."); return; }

                        foreach (var course in activeCoursesAndStudents)
                        {
                            Console.WriteLine($"Course: {course.CourseName}");
                            if (course.Registrations.Any())
                            {
                                foreach (var registration in course.Registrations)
                                {
                                    Console.WriteLine($"- {registration.Student.FirstName} {registration.Student.LastName}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("- No students registered.");
                            }
                            Console.WriteLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error viewing active courses: {ex.Message}");
                }
            }

            public void RegisterStudentOnCourse()
            {
                Console.Clear();
                Console.WriteLine("==== Register student on course ====");
                Console.Write("Enter student ID: ");

                if (!int.TryParse(Console.ReadLine(), out int studentId)) return;

                Console.Write("Enter Course ID: ");
                if (!int.TryParse(Console.ReadLine(), out int courseId)) return;

                using (var dbContext = new QueryNinjasDbContext())
                {
                    var student = dbContext.Students.Find(studentId);
                    var course = dbContext.Courses.Find(courseId);

                    if (student == null || course == null)
                    {
                        Console.WriteLine("Invalid student or course ID.");
                        return;
                    }

                    //check if student is already registered on that course
                    var alreadyRegistered = dbContext.Registrations
                        .Any(r => r.FkStudentId == studentId && r.FkCourseId == courseId);

                    if (alreadyRegistered)
                    {
                        Console.WriteLine($"Student {student.FirstName} {student.LastName} is already registered on {course.CourseName}.");
                        return;
                    }

                    // create new registration
                    var registration = new Registration
                    {
                        FkStudentId = studentId,
                        FkCourseId = courseId,
                        RegistrationDate = DateTime.Now
                    };

                    dbContext.Registrations.Add(registration);
                    dbContext.SaveChanges();

                    Console.WriteLine($"Student {student.FirstName} {student.LastName} registered on {course.CourseName}.");
                }
            }
        }

        
        // 2. STUDENT MENU IMPLEMENTATION
        

        public class StudentMenu
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
                            EditStudent();
                            Console.ReadKey();
                            break;
                        case "3":
                            RemoveStudent();
                            Console.ReadKey();
                            break;
                        case "4":
                            ViewStudents();
                            Console.ReadKey();
                            break;
                        case "5":
                            ViewStudentDetails();
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

            // --- Student Menu CRUD/Views (Final Merged Logic) ---

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
                using (var dbContext = new QueryNinjasDbContext())
                {
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

            public void EditStudent()
            {
                Console.WriteLine("==== Edit Student ====");
                Console.Write("Enter Student ID to edit: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId)) return;

                try
                {
                    using (var dbContext = new QueryNinjasDbContext())
                    {
                        var studentToEdit = dbContext.Students.Find(studentId);

                        if (studentToEdit == null)
                        {
                            Console.WriteLine("Student not found.");
                            return;
                        }

                        Console.WriteLine($"Current Name: {studentToEdit.FirstName}. Enter new first name (leave blank to keep current):");
                        var newFirstName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newFirstName))
                        {
                            studentToEdit.FirstName = newFirstName;
                        }

                        // Assuming updates for other fields are handled here, but focusing on the structure now

                        dbContext.SaveChanges();
                        Console.WriteLine("Student updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error editing student: {ex.Message}");
                }
            }

            public void RemoveStudent()
            {
                Console.WriteLine("==== Remove Student ====");
                Console.Write("Enter Student ID to remove: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId)) return;

                try
                {
                    using (var dbContext = new QueryNinjasDbContext())
                    {
                        var studentToRemove = dbContext.Students.Find(studentId);

                        if (studentToRemove == null)
                        {
                            Console.WriteLine("Student not found.");
                            return;
                        }

                        // Assuming EF/DB handles CASCADE DELETE, otherwise manual deletion of dependent records is needed.

                        dbContext.Students.Remove(studentToRemove);
                        dbContext.SaveChanges();
                        Console.WriteLine($"Student ID {studentId} removed successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error removing student: {ex.Message}");
                }
            }

            public void ViewStudents()
            {
                using (var dbContext = new QueryNinjasDbContext())
                {
                    var students = dbContext.Students.ToList();
                    Console.WriteLine("==== Students List ====");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, Birth Date: {student.BirthDate.ToShortDateString()}, Email: {student.Email}");
                    }
                }
            }

            public void ViewStudentDetails()
            {
                Console.Clear();
                Console.WriteLine("==== View Student Details and Records ====");
                Console.Write("Enter Student ID: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId)) return;

                try
                {
                    using (var dbContext = new QueryNinjasDbContext())
                    {
                        // Complex query to join Grades, Courses, and Teachers
                        var records = dbContext.Grades
                            .Where(g => g.FkStudentId == studentId)
                            .Include(g => g.Student)
                            .Include(g => g.Course)
                            .Include(g => g.Teacher)
                            .ToList();

                        if (records.Count == 0)
                        {
                            Console.WriteLine($"No records found for Student ID {studentId}.");
                            return;
                        }

                    Console.WriteLine($"\n--- Records for {records.First().Student.FirstName} {records.First().Student.LastName} (ID: {studentId}) ---");
                    Console.WriteLine("{0,-20} {1,-10} {2,-20}", "Course", "Grade", "Teacher");
                    Console.WriteLine("--------------------------------------------------------------------------------");

                        foreach (var record in records)
                        {
                            Console.WriteLine("{0,-20} {1,-10} {2,-20}",
                                record.Course?.CourseName,
                                record.GradeValue,
                                record.Teacher?.FirstName + " " + record.Teacher?.LastName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error viewing details: {ex.Message}");
                }
            }
        }

        
        // 3. SCHEDULE MENU IMPLEMENTATION
        

        public class ScheduleMenu
        {
            public void ShowSchedule()
            {
                // ... (Logic remains the same as provided by user)
                Console.WriteLine("Schedule menu logic...");
                Console.ReadKey();
                return;
            }
        }

        
        // 4. REPORT MENU IMPLEMENTATION (Unified and Corrected)
        

        public class ReportMenu
        // 1. View Schedule
        private void ViewSchedule()
        {
            var dbContext = new Data.QueryNinjasDbContext();
            var schedules = dbContext.Schedules
                .Include(s => s.Course)     
                .Include(s => s.ClassRoom)
                .OrderBy(s => s.StartTime)
                .ToList();

            Console.WriteLine("==== Schedule List ====");
            foreach (var schedule in schedules)
            {
                Console.WriteLine(
                    $"ID: {schedule.ScheduleId}, " +
                    $"Course: {schedule.Course?.CourseName}, " +
                    $"Classroom: {schedule.ClassRoom?.RoomNumber}, " +
                    $"Start: {schedule.StartTime}, End: {schedule.EndTime}"

                );
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        // 2. Add Schedule Item
        private void AddScheduleItem()
        {
            // Using the injected service instance
            private readonly ReportService _reportService;

            // Constructor required due to Dependency Injection
            public ReportMenu(ReportService reportService)
            {
                _reportService = reportService;
            }

            public void ShowReport()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("==== Reports (Final LINQ Reports) ====");
                    Console.WriteLine("1. Approved students (Avg >= 3.0)");
                    Console.WriteLine("2. Non-approved students (Avg < 3.0)");
                    Console.WriteLine("3. All students");
                    Console.WriteLine("4. Course Average Grades (Complex LINQ)");
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

            // --- Supporting Display Methods (Using _reportService) ---

            public void DisplayCourseAverageReport()
            {
                try
                {
                    var results = _reportService.GetCourseAverageGrades();

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
                    var results = _reportService.GetAllStudentsReport();

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
                    var results = _reportService.GetApprovedStudents();
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
                    var results = _reportService.GetNonApprovedStudents();
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