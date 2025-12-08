using QueryNinja.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QueryNinja.UI.UserInterFace.CourseMenu;

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

                try
                {
                    var dbContext = new QueryNinjasDbContext();
                    var course = new Course
                    {
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
                    var dbContext = new QueryNinjasDbContext();
                    var courses = dbContext.Courses.ToList();

                    if (courses.Count == 0) { Console.WriteLine("No courses found."); return; }

                    foreach (var course in courses)
                    {
                        Console.WriteLine($"ID: {course.CourseId}, Name: {course.CourseName}, Start: {course.StartDate.ToShortDateString()}, End: {course.EndDate.ToShortDateString()}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error viewing courses: {ex.Message}");
                }
            }

            // 3. View Active Courses
            public void ViewActiveCourses()
            {
                Console.Clear();
                Console.WriteLine("==== Active Courses ====");
                try
                {
                    var today = DateTime.Today;
                    var dbContext = new QueryNinjasDbContext();
                    var activeCourses = dbContext.Courses
                        .Where(c => c.StartDate <= today && c.EndDate >= today)
                        .ToList();

                    if (activeCourses.Count == 0) { Console.WriteLine("No active courses found."); return; }

                    foreach (var course in activeCourses)
                    {
                        Console.WriteLine($"ID: {course.CourseId}, Name: {course.CourseName}, End: {course.EndDate.ToShortDateString()}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error viewing active courses: {ex.Message}");
                }
            }
            public void ShowCourse()
            {
                while (true)
                {
                    
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
                            0           RegisterStudentOnCourse(); 
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

            // student menu
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
                        Console.WriteLine("5. View student details"); //Som administratör vill jag kunna se elever, vilka kurser de läst, vilka betyg de fått och vilken lärare som satt betyget samt datum när betyget satts.
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
                                //EditStudent();
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "3":
                                //RemoveStudent();
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "4":
                                ViewStudents();
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                break;
                            case "5":
                                //ViewStudentDetails();
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
                    var dbContext = new Data.QueryNinjasDbContext();
                    var students = dbContext.Students.ToList();
                    Console.WriteLine("==== Students List ====");
                    foreach (var student in students)
                    {
                        Console.WriteLine($"ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}, Birth Date: {student.BirthDate.ToShortDateString()}, Email: {student.Email}");
                    }
                }

                // method to add a new student
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
                    var dbContext = new Data.QueryNinjasDbContext();
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

            // 2. Edit Student (UPDATE)
            public void EditStudent()
            {
                Console.WriteLine("==== Edit Student ====");
                Console.Write("Enter Student ID to edit: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId)) return;

                try
                {
                    var dbContext = new QueryNinjasDbContext();
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

                    dbContext.SaveChanges();
                    Console.WriteLine("Student updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error editing student: {ex.Message}");
                }
            }

            // 3. Remove Student (DELETE)
            public void RemoveStudent()
            {
                Console.WriteLine("==== Remove Student ====");
                Console.Write("Enter Student ID to remove: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId)) return;

                try
                {
                    var dbContext = new QueryNinjasDbContext();
                    var studentToRemove = dbContext.Students.Find(studentId);

                    if (studentToRemove == null)
                    {
                        Console.WriteLine("Student not found.");
                        return;
                    }

                    dbContext.Students.Remove(studentToRemove);
                    dbContext.SaveChanges();
                    Console.WriteLine($"Student ID {studentId} removed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error removing student: {ex.Message}");
                }
            }

            // 5. View Student Details (Complex READ/JOIN)
            public void ViewStudentDetails()
            {
                Console.WriteLine("==== View Student Details and Records ====");
                Console.Write("Enter Student ID: ");
                if (!int.TryParse(Console.ReadLine(), out int studentId)) return;

                try
                {
                    var dbContext = new QueryNinjasDbContext();

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
                    Console.WriteLine("{0,-20} {1,-10} {2,-20} {3}", "Course", "Grade", "Teacher", "Date Set");
                    Console.WriteLine("--------------------------------------------------------------------------------");

                    foreach (var record in records)
                    {
                        Console.WriteLine("{0,-20} {1,-10} {2,-20} {3}",
                            record.Course.CourseName,
                            record.GradeValue,
                            record.Teacher.FirstName + " " + record.Teacher.LastName,
                            record.DateSet.ToShortDateString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error viewing details: {ex.Message}");
                }
            }
        }

        // schema menu
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
            // raport menu
            public class ReportMenu
            {
                public void ShowReport()
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("==== Reports ====");
                        Console.WriteLine("1. Approved students");
                        Console.WriteLine("2. Non-approved students");
                        Console.WriteLine("3. All students");
                        Console.WriteLine("0. Back");
                        Console.Write("Choice: ");
                        var input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                Console.WriteLine("Generate report not implemented yet.");
                                Console.ReadKey();
                                break;
                            case "2":
                                Console.WriteLine("Generate report not implemented yet.");
                                Console.ReadKey();
                                break;
                            case "3":
                                Console.WriteLine("Generate report not implemented yet.");
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
        }
    }
}
