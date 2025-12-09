using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QueryNinja.Models;

namespace QueryNinja.Data
{
    public class QueryNinjasDbContext : DbContext
    {
        public QueryNinjasDbContext()
        {
        }

        public QueryNinjasDbContext(DbContextOptions<QueryNinjasDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<StoredProcedureResult> StoredProcedureResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                  "Server=localhost;Database=Query-ninjas;Trusted_Connection=True;TrustServerCertificate=True;"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoredProcedureResult>().HasNoKey();

            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Student>().ToTable("Student");

            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.Email)
                .IsUnique();

            modelBuilder.Entity<ClassRoom>()
                .HasIndex(c => c.RoomNumber)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.CourseName)
                .IsUnique();

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Teacher)
                .WithMany(t => t.Grades)
                .HasForeignKey(g => g.FkTeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.FkStudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.FkCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { TeacherId = 1, FirstName = "Karin", LastName = "Lund", Email = "karin.lund@school.com", AreaOfExpertise = "Backend Development" },
                new Teacher { TeacherId = 2, FirstName = "Peter", LastName = "Sund", Email = "peter.sund@school.com", AreaOfExpertise = "Databases" },
                new Teacher { TeacherId = 3, FirstName = "Maria", LastName = "Ekström", Email = "maria.ekstrom@school.com", AreaOfExpertise = "Frontend Development" },
                new Teacher { TeacherId = 4, FirstName = "Jonas", LastName = "Björk", Email = "jonas.bjork@school.com", AreaOfExpertise = "DevOps" }
            );

            modelBuilder.Entity<ClassRoom>().HasData(
                new ClassRoom { ClassRoomId = 1, RoomNumber = 101 },
                new ClassRoom { ClassRoomId = 2, RoomNumber = 102 },
                new ClassRoom { ClassRoomId = 3, RoomNumber = 103 },
                new ClassRoom { ClassRoomId = 4, RoomNumber = 104 },
                new ClassRoom { ClassRoomId = 5, RoomNumber = 105 },
                new ClassRoom { ClassRoomId = 6, RoomNumber = 106 },
                new ClassRoom { ClassRoomId = 7, RoomNumber = 107 },
                new ClassRoom { ClassRoomId = 8, RoomNumber = 108 },
                new ClassRoom { ClassRoomId = 9, RoomNumber = 109 },
                new ClassRoom { ClassRoomId = 10, RoomNumber = 110 }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseName = "C# Programming", StartDate = new DateTime(2025, 1, 15, 0, 0, 0), EndDate = new DateTime(2025, 6, 15, 0, 0, 0), FkTeacherId = 1 },
                new Course { CourseId = 2, CourseName = "Database Design", StartDate = new DateTime(2025, 1, 15, 0, 0, 0), EndDate = new DateTime(2025, 6, 15, 0, 0, 0), FkTeacherId = 2 },
                new Course { CourseId = 3, CourseName = "Web Development", StartDate = new DateTime(2025, 8, 20, 0, 0, 0), EndDate = new DateTime(2025, 12, 20, 0, 0, 0), FkTeacherId = 3 },
                new Course { CourseId = 4, CourseName = "Software Architecture", StartDate = new DateTime(2025, 8, 20, 0, 0, 0), EndDate = new DateTime(2025, 12, 20, 0, 0, 0), FkTeacherId = 1 },
                new Course { CourseId = 5, CourseName = "Agile Methods", StartDate = new DateTime(2025, 1, 15, 0, 0, 0), EndDate = new DateTime(2025, 6, 15, 0, 0, 0), FkTeacherId = 4 },
                new Course { CourseId = 6, CourseName = "JavaScript Basics", StartDate = new DateTime(2025, 8, 20, 0, 0, 0), EndDate = new DateTime(2025, 12, 20, 0, 0, 0), FkTeacherId = 2 },
                new Course { CourseId = 7, CourseName = "Cloud Computing", StartDate = new DateTime(2025, 1, 15, 0, 0, 0), EndDate = new DateTime(2025, 6, 15, 0, 0, 0), FkTeacherId = 3 },
                new Course { CourseId = 8, CourseName = "DevOps", StartDate = new DateTime(2025, 8, 20, 0, 0, 0), EndDate = new DateTime(2025, 12, 20, 0, 0, 0), FkTeacherId = 4 },
                new Course { CourseId = 9, CourseName = "UML & Modeling", StartDate = new DateTime(2025, 1, 15, 0, 0, 0), EndDate = new DateTime(2025, 6, 15, 0, 0, 0), FkTeacherId = 1 },
                new Course { CourseId = 10, CourseName = "Security Fundamentals", StartDate = new DateTime(2025, 8, 20, 0, 0, 0), EndDate = new DateTime(2025, 12, 20, 0, 0, 0), FkTeacherId = 2 }
            );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { ScheduleId = 1, FkCourseId = 1, FkClassRoomId = 1, StartTime = new DateTime(2025, 1, 15, 9, 0, 0), EndTime = new DateTime(2025, 1, 15, 11, 0, 0) },
                new Schedule { ScheduleId = 2, FkCourseId = 2, FkClassRoomId = 2, StartTime = new DateTime(2025, 1, 15, 13, 0, 0), EndTime = new DateTime(2025, 1, 15, 15, 0, 0) },
                new Schedule { ScheduleId = 3, FkCourseId = 3, FkClassRoomId = 3, StartTime = new DateTime(2025, 8, 20, 10, 0, 0), EndTime = new DateTime(2025, 8, 20, 12, 0, 0) },
                new Schedule { ScheduleId = 4, FkCourseId = 4, FkClassRoomId = 4, StartTime = new DateTime(2025, 8, 20, 14, 0, 0), EndTime = new DateTime(2025, 8, 20, 16, 0, 0) },
                new Schedule { ScheduleId = 5, FkCourseId = 5, FkClassRoomId = 5, StartTime = new DateTime(2025, 1, 15, 8, 0, 0), EndTime = new DateTime(2025, 1, 15, 10, 0, 0) },
                new Schedule { ScheduleId = 6, FkCourseId = 6, FkClassRoomId = 6, StartTime = new DateTime(2025, 8, 20, 11, 0, 0), EndTime = new DateTime(2025, 8, 20, 13, 0, 0) },
                new Schedule { ScheduleId = 7, FkCourseId = 7, FkClassRoomId = 7, StartTime = new DateTime(2025, 1, 15, 15, 0, 0), EndTime = new DateTime(2025, 1, 15, 17, 0, 0) },
                new Schedule { ScheduleId = 8, FkCourseId = 8, FkClassRoomId = 8, StartTime = new DateTime(2025, 8, 20, 9, 0, 0), EndTime = new DateTime(2025, 8, 20, 11, 0, 0) },
                new Schedule { ScheduleId = 9, FkCourseId = 9, FkClassRoomId = 9, StartTime = new DateTime(2025, 1, 15, 13, 0, 0), EndTime = new DateTime(2025, 1, 15, 15, 0, 0) },
                new Schedule { ScheduleId = 10, FkCourseId = 10, FkClassRoomId = 10, StartTime = new DateTime(2025, 8, 20, 10, 0, 0), EndTime = new DateTime(2025, 8, 20, 12, 0, 0) }
            );

            // Seed-data, Students (AI-generated)
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentID = 1, FirstName = "Anna", LastName = "Svensson", BirthDate = new DateTime(2000, 5, 12), Email = "anna.svensson@student.com" },
                new Student { StudentID = 2, FirstName = "Erik", LastName = "Johansson", BirthDate = new DateTime(1999, 11, 3), Email = "erik.johansson@student.com" },
                new Student { StudentID = 3, FirstName = "Sara", LastName = "Lindgren", BirthDate = new DateTime(2001, 2, 20), Email = "sara.lindgren@student.com" },
                new Student { StudentID = 4, FirstName = "David", LastName = "Berg", BirthDate = new DateTime(1998, 7, 8), Email = "david.berg@student.com" },
                new Student { StudentID = 5, FirstName = "Emma", LastName = "Nyström", BirthDate = new DateTime(2000, 9, 15), Email = "emma.nystrom@student.com" },
                new Student { StudentID = 6, FirstName = "Johan", LastName = "Persson", BirthDate = new DateTime(1999, 4, 25), Email = "johan.persson@student.com" },
                new Student { StudentID = 7, FirstName = "Maja", LastName = "Andersson", BirthDate = new DateTime(2001, 12, 1), Email = "maja.andersson@student.com" },
                new Student { StudentID = 8, FirstName = "Niklas", LastName = "Karlsson", BirthDate = new DateTime(1998, 6, 30), Email = "niklas.karlsson@student.com" },
                new Student { StudentID = 9, FirstName = "Elin", LastName = "Holm", BirthDate = new DateTime(2000, 3, 10), Email = "elin.holm@student.com" },
                new Student { StudentID = 10, FirstName = "Oscar", LastName = "Wikström", BirthDate = new DateTime(1999, 8, 22), Email = "oscar.wikstrom@student.com" },
                new Student { StudentID = 11, FirstName = "Karin", LastName = "Åberg", BirthDate = new DateTime(2001, 1, 5), Email = "karin.aberg@student.com" },
                new Student { StudentID = 12, FirstName = "Mattias", LastName = "Forsberg", BirthDate = new DateTime(1998, 10, 14), Email = "mattias.forsberg@student.com" },
                new Student { StudentID = 13, FirstName = "Linda", LastName = "Ström", BirthDate = new DateTime(2000, 4, 18), Email = "linda.strom@student.com" },
                new Student { StudentID = 14, FirstName = "Patrik", LastName = "Hellgren", BirthDate = new DateTime(1999, 7, 27), Email = "patrik.hellgren@student.com" },
                new Student { StudentID = 15, FirstName = "Sofia", LastName = "Blom", BirthDate = new DateTime(2001, 11, 9), Email = "sofia.blom@student.com" },
                new Student { StudentID = 16, FirstName = "Andreas", LastName = "Lundqvist", BirthDate = new DateTime(1998, 2, 2), Email = "andreas.lundqvist@student.com" },
                new Student { StudentID = 17, FirstName = "Helena", LastName = "Sandberg", BirthDate = new DateTime(2000, 6, 6), Email = "helena.sandberg@student.com" },
                new Student { StudentID = 18, FirstName = "Marcus", LastName = "Viklund", BirthDate = new DateTime(1999, 12, 19), Email = "marcus.viklund@student.com" },
                new Student { StudentID = 19, FirstName = "Ida", LastName = "Norberg", BirthDate = new DateTime(2001, 8, 23), Email = "ida.norberg@student.com" },
                new Student { StudentID = 20, FirstName = "Per", LastName = "Häggström", BirthDate = new DateTime(1998, 3, 29), Email = "per.haggstrom@student.com" }
            );

            // Seed-data, Grades (AI-generated) 
            modelBuilder.Entity<Grade>().HasData(
                new Grade { GradeId = 1, FkStudentId = 1, FkCourseId = 1, FkTeacherId = 1, GradeValue = "VG" },
                new Grade { GradeId = 2, FkStudentId = 2, FkCourseId = 2, FkTeacherId = 2, GradeValue = "G" },
                new Grade { GradeId = 5, FkStudentId = 9, FkCourseId = 9, FkTeacherId = 1, GradeValue = "G" },
                new Grade { GradeId = 7, FkStudentId = 11, FkCourseId = 1, FkTeacherId = 1, GradeValue = "G" },
                new Grade { GradeId = 8, FkStudentId = 12, FkCourseId = 2, FkTeacherId = 2, GradeValue = "VG" },
                new Grade { GradeId = 10, FkStudentId = 19, FkCourseId = 9, FkTeacherId = 1, GradeValue = "VG" }
            );

            modelBuilder.Entity<Registration>().HasData(
                new Registration { RegistrationId = 7, FkStudentId = 3, FkCourseId = 3, RegistrationDate = new DateTime(2025, 8, 15) },
                new Registration { RegistrationId = 8, FkStudentId = 4, FkCourseId = 4, RegistrationDate = new DateTime(2025, 8, 16) },
                new Registration { RegistrationId = 9, FkStudentId = 10, FkCourseId = 10, RegistrationDate = new DateTime(2025, 8, 19) },
                new Registration { RegistrationId = 10, FkStudentId = 18, FkCourseId = 8, RegistrationDate = new DateTime(2025, 8, 18) },
                new Registration { RegistrationId = 11, FkStudentId = 20, FkCourseId = 10, RegistrationDate = new DateTime(2025, 8, 20) },
                new Registration { RegistrationId = 21, FkStudentId = 5, FkCourseId = 3, RegistrationDate = new DateTime(2025, 8, 22) },
                new Registration { RegistrationId = 22, FkStudentId = 6, FkCourseId = 4, RegistrationDate = new DateTime(2025, 8, 23) },
                new Registration { RegistrationId = 23, FkStudentId = 7, FkCourseId = 6, RegistrationDate = new DateTime(2025, 8, 24) },
                new Registration { RegistrationId = 24, FkStudentId = 8, FkCourseId = 8, RegistrationDate = new DateTime(2025, 8, 25) },
                new Registration { RegistrationId = 25, FkStudentId = 13, FkCourseId = 10, RegistrationDate = new DateTime(2025, 8, 26) },
                new Registration { RegistrationId = 26, FkStudentId = 14, FkCourseId = 3, RegistrationDate = new DateTime(2025, 8, 27) },
                new Registration { RegistrationId = 27, FkStudentId = 15, FkCourseId = 4, RegistrationDate = new DateTime(2025, 8, 28) },
                new Registration { RegistrationId = 28, FkStudentId = 16, FkCourseId = 6, RegistrationDate = new DateTime(2025, 8, 29) },
                new Registration { RegistrationId = 29, FkStudentId = 17, FkCourseId = 8, RegistrationDate = new DateTime(2025, 8, 30) }
            );
        } 
    } 
} 