using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace student_registration.Models
{
    public class ITIContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Department> Departments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database = ITI_MVC; TrustServerCertificate=True; Trusted_Connection = True;");
            base.OnConfiguring(optionsBuilder);
        }
    
    }
}
