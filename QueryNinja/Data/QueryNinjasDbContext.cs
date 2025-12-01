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

        public DbSet<Teacher> Teachers { get; set; }

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
