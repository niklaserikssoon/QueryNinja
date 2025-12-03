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
            : base(new DbContextOptionsBuilder<QueryNinjasDbContext>()
                      .UseSqlServer("Server=localhost;Database=QueryNinjasDb;Trusted_Connection=True;")
                      .Options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<ClassRoom> ClassRooms { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=QueryNinjasDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //unique index
            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.Email)
                .IsUnique();

            modelBuilder.Entity<ClassRoom>()
                .HasIndex(c => c.RoomNumber)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.CourseName)
                .IsUnique();

            //Seed-data, Teachers (AI-generated)
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { TeacherId = 1, FirstName = "Karin", LastName = "Lund", Email = "karin.lund@school.com", AreaOfExpertise = "Backend Development" },
                new Teacher { TeacherId = 2, FirstName = "Peter", LastName = "Sund", Email = "peter.sund@school.com", AreaOfExpertise = "Databases" },
                new Teacher { TeacherId = 3, FirstName = "Maria", LastName = "Ekström", Email = "maria.ekstrom@school.com", AreaOfExpertise = "Frontend Development" },
                new Teacher { TeacherId = 4, FirstName = "Jonas", LastName = "Björk", Email = "jonas.bjork@school.com", AreaOfExpertise = "DevOps" }
            );

            //Seed-data, ClassRooms (AI-generated)
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

            //Seed-data, Courses (AI-generated)
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

            //Seed-data, Schedules (AI-generated)
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

               //Seeddata för student och registration saknas. Lägg till när student hämtats från SSMS 
        }
    }
}
