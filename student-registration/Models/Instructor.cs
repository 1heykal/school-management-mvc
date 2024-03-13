using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class Instructor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly HireDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }

}
