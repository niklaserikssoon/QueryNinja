using QueryNinja.Models;
using System;
using System.Collections.Generic;
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
                Console.WriteLine("1. Courseadministration");
                Console.WriteLine("2. students");
                Console.WriteLine("3. Schema");
                Console.WriteLine("4. Raports");
                Console.WriteLine("0. quit");
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
                    Console.WriteLine("==== Course Administration ====");
                    Console.WriteLine("1. Create course");
                    Console.WriteLine("2. View courses");
                    Console.WriteLine("3. View active courses");
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
                        Console.WriteLine("==== Student Menu ====");
                        Console.WriteLine("1. Add student");
                        Console.WriteLine("2. View students");
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
                                ViewStudents();
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

            // schema menu
            public class ScheduleMenu
            {
                public void ShowSchedule()
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("==== Schedule Menu ====");
                        Console.WriteLine("1. View schedule");
                        Console.WriteLine("0. Back");
                        Console.Write("Choice: ");
                        var input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                Console.WriteLine("View schedule not implemented yet.");
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
                        Console.WriteLine("==== Report Menu ====");
                        Console.WriteLine("1. Generate report");
                        Console.WriteLine("0. Back");
                        Console.Write("Choice: ");
                        var input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
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
