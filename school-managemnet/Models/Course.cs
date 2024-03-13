using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public double DuartionInHours { get; set; }
    }
}
