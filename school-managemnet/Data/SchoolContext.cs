using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models;

namespace SchoolManagement.Data
{
    public class SchoolContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Lab> Labs { get; set; }

        public SchoolContext() : base() { }

        public SchoolContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().UseTptMappingStrategy();
            builder.Entity<Instructor>().Property<decimal>(i => i.Salary).HasPrecision(38, 18);
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.; Database = SchoolDb; TrustServerCertificate=True; Trusted_Connection = True;");
                base.OnConfiguring(optionsBuilder);
            }
        }

    }
}
