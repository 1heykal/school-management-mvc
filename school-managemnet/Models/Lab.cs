using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models
{
    public class Lab
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
