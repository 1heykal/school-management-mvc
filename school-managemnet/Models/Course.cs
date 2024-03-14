using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public double DuartionInHours { get; set; }
        public int LabId { get; set; }

        [ForeignKey(nameof(LabId))]
        public Lab Lab { get; set; }

        public ICollection<Department> Departments { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
    }
}
