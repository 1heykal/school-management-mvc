using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace SchoolManagement.Models
{
    public class Instructor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public string Gender { get; set; }
        public DateOnly HireDate { get; set; }
        public ICollection<Course> Courses { get; set; }
        public int DepartemntId { get; set; }

        [ForeignKey(nameof(DepartemntId))]
        public Department Department { get; set; }
    }

}
