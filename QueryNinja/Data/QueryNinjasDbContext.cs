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
        public QueryNinjasDbContext(DbContextOptions<QueryNinjasDbContext> options)
            : base(options)
        {
        }

        public QueryNinjasDbContext()
            : base(new DbContextOptions<QueryNinjasDbContext>())
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<StoredProcedureResult> StoredProcedureResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=QueryNinjasDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.Email)
                .IsUnique();
        }
    }
}
