using QueryNinja.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryNinja.Service;
using QueryNinja.Data;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

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


            // method to create a new course
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
                    var dbContext = new QueryNinjasDbContext();
                    var course = new Course
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

            // method to view all courses
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
                Console.WriteLine("==== Active courses and students ====");
                try
                {
                    var today = DateTime.Today;
                    Console.WriteLine($"Today is: {today}\n");
                    var dbContext = new QueryNinjasDbContext();
                    var activeCoursesAndStudents = dbContext.Courses
                        .Join(dbContext.Registrations,
                        c => c.CourseId,
                        r => r.FkCourseId,
                        (c, r) => new { c, r })
                        .Join(dbContext.Students,
                        cr => cr.r.FkStudentId,
                        s => s.StudentID,
                        (cr, s) => new
                        {
                            Course = cr.c,
                            Student = s
                        })

                        .Where(crs => crs.Course.StartDate <= today && crs.Course.EndDate >= today)
                        .GroupBy(crs => crs.Course.CourseName)
                        .ToList();

                    if (activeCoursesAndStudents.Count == 0) { Console.WriteLine("No active courses found."); return; }

                    foreach (var courseGroup in activeCoursesAndStudents)
                    {
                        Console.WriteLine($"Course: {courseGroup.Key}");
                        foreach (var item in courseGroup)
                        {
                            Console.WriteLine($"- {item.Student.FirstName} {item.Student.LastName}");
                        }
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error viewing active courses: {ex.Message}");
                }
            }

            // 4. Register Student On Course

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
                
    }
        public class studentMenu
        {
            public void ShowStudent()
            {
                while (true)
                {
                    // student administration menu
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
                // 1. Add Student (CREATE)
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
        // 4. View Students (READ)
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

            // 5. View Student Details (Complex READ/JOIN)
            public void ViewStudentDetails()
            {
                Console.Clear();
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
                    Console.WriteLine("{0,-20} {1,-10} {2,-20}", "Course", "Grade", "Teacher");
                    Console.WriteLine("--------------------------------------------------------------------------------");

                    foreach (var record in records)
                    {
                        Console.WriteLine("{0,-20} {1,-10} {2,-20}",
                            record.Course.CourseName,
                            record.GradeValue,
                            record.Teacher.FirstName + " " + record.Teacher.LastName);
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
                        ViewSchedule();
                        break;

                    case "2":
                        AddScheduleItem();
                        break;

                    case "3":
                        ManageTeachers();
                        break;

                    case "4":
                        ManageClassrooms();
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
            Console.Clear();
            Console.WriteLine("=== Add Schedule Item ===");

            Console.Write("Enter course ID: ");
            int courseId = int.Parse(Console.ReadLine());

            Console.Write("Enter classroom ID: ");
            int classRoomId = int.Parse(Console.ReadLine());

            Console.Write("Enter start time (yyyy-mm-dd HH:mm): ");
            DateTime startTime;
            while (!DateTime.TryParse(Console.ReadLine(), out startTime))
            {
                Console.Write("Invalid format. Try again (yyyy-mm-dd HH:mm): ");
            }

            Console.Write("Enter end time (yyyy-mm-dd HH:mm): ");
            DateTime endTime;
            while (!DateTime.TryParse(Console.ReadLine(), out endTime))
            {
                Console.Write("Invalid format. Try again (yyyy-mm-dd HH:mm): ");
            }

            using (var dbContext = new Data.QueryNinjasDbContext())
            {
                var newSchedule = new Schedule
                {
                    FkCourseId = courseId,
                    FkClassRoomId = classRoomId,
                    StartTime = startTime,
                    EndTime = endTime
                };

                dbContext.Schedules.Add(newSchedule);
                dbContext.SaveChanges();
            }

            Console.WriteLine("Schedule item saved to database!");
            Console.ReadKey();

        }
        // 3. Manage Teachers
        private void ManageTeachers()
        {
            Console.Clear();
            var dbContext = new QueryNinjasDbContext();
            Console.WriteLine("==== Manage Teachers ====");
            Console.WriteLine("1. Add teacher");
            Console.WriteLine("2. Edit teacher");
            Console.WriteLine("3. Remove teacher");
            Console.WriteLine("4. View all teachers");
            Console.WriteLine("0. Back");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter teacher first name: ");
                    var firstName = Console.ReadLine();
                    Console.Write("Enter teacher last name: ");
                    var lastName = Console.ReadLine();
                    Console.Write("Enter teacher email: ");
                    var email = Console.ReadLine();
                    if (dbContext.Teachers.Any(t => t.Email == email))
                    {
                        Console.WriteLine("This email is already registered. Email must be unique.");
                        return;
                    }
                    Console.Write("Enter area of expertise (not mandatory, max 1 per teacher): ");
                    var aOfExpertise = Console.ReadLine();

                    var teacher = new Teacher
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        AreaOfExpertise = aOfExpertise
                    };
                    dbContext.Teachers.Add(teacher);
                    dbContext.SaveChanges();
                    Console.WriteLine("Teacher added successfully.");
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Write("Enter teacherId to edit: ");
                    var teacherIdInput = int.TryParse(Console.ReadLine(), out int teacherId);
                    var teacherToEdit = dbContext.Teachers.Find(teacherId);
                    if (teacherToEdit == null)
                    {
                        Console.WriteLine("Teacher not found.");
                        break;
                    }

                    Console.WriteLine("What do you want to edit? ");
                    Console.WriteLine("1. First name");
                    Console.WriteLine("2. Last name");
                    Console.WriteLine("3. Email");
                    Console.WriteLine("4. Area of expertise");
                    Console.WriteLine("0. Back");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1": 
                            Console.Write("Enter new first name: ");
                            teacherToEdit.FirstName = Console.ReadLine();
                            break;

                        case "2":
                            Console.Write("Enter new last name: ");
                            teacherToEdit.LastName = Console.ReadLine();
                            break;

                        case "3":
                            Console.Write("Enter new email: ");
                            teacherToEdit.Email = Console.ReadLine();
                            break;

                        case "4":
                            Console.Write("Enter new area of expertise: ");
                            teacherToEdit.AreaOfExpertise = Console.ReadLine();
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            Console.ReadKey();
                            break;
                    }

                    dbContext.SaveChanges();
                    Console.WriteLine("Teacher updated successfully.");
                    Console.ReadKey();
                    break;


                case "3":
                    Console.WriteLine("Enter teacherId to remove: ");
                    if (!int.TryParse(Console.ReadLine(), out int removeTeacherId))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        Console.ReadKey();
                        break;
                    }
                    var teacherToRemove = dbContext.Teachers.Find(removeTeacherId);
                    if (teacherToRemove == null)
                    {
                        Console.WriteLine("Teacher not found.");
                        Console.ReadKey();
                        break;
                    }

                    Console.WriteLine($"Are you sure that you want to remove {teacherToRemove.FirstName} {teacherToRemove.LastName}? (Y/N)");
                    var decision= Console.ReadLine(); 

                    if (decision?.ToUpper() == "Y")
                    {
                        dbContext.Teachers.Remove(teacherToRemove);
                        dbContext.SaveChanges();
                        Console.WriteLine("Teacher removed successfully.");
                        Console.ReadKey(); 
                    }

                    else
                    {
                        Console.WriteLine("Operation cancelled");
                        Console.ReadKey();
                    }
                    break;

                case "4":
                    var teachers = dbContext.Teachers.ToList();
                    Console.WriteLine("==== Teachers List ====");
                    foreach (var t in teachers)
                    {
                        Console.WriteLine($"ID: {t.TeacherId}, Name: {t.FirstName} {t.LastName}, Email: {t.Email}, Area of expertise: {t.AreaOfExpertise}");
                    };
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
        // 4. Manage Classrooms
        private void ManageClassrooms()
        {
            Console.Clear();
            var dbContext = new QueryNinjasDbContext();
            Console.WriteLine("==== Manage Classrooms ====");
            Console.WriteLine("1. Add classroom");
            Console.WriteLine("2. View all classrooms");
            Console.WriteLine("0. Back");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter room number: ");
                    var roomNumber = int.TryParse(Console.ReadLine(), out int roomNr);
                    if (dbContext.ClassRooms.Any(c => c.RoomNumber == roomNr))
                    {
                        Console.WriteLine("Room number already exists. The number must be unique.");
                        Console.ReadKey();
                        return;
                    }
                    
                    var classRoom = new ClassRoom
                    {
                        RoomNumber = roomNr
                    };
                    dbContext.ClassRooms.Add(classRoom);
                    dbContext.SaveChanges();
                    Console.WriteLine("Classroom added successfully.");
                    Console.ReadKey();
                    break;

                case "2":
                    var rooms = dbContext.ClassRooms.ToList();
                    Console.WriteLine("==== List of Classrooms ====");
                    foreach (var room in rooms)
                    {
                        Console.WriteLine($"ID: {room.ClassRoomId}, Room Number: {room.RoomNumber}");
                    }
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
        // Reports menu
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
        // 1. Active Courses Report
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
        // 2. Student Overview Report
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
        // 3. Register Student via Stored Procedure
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

