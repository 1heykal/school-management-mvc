using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models;

public class Enrollment
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    
    [DisplayFormat(NullDisplayText = "No grade")]
    public Grade? Grade { get; set; }
    
    public Course Course { get; set; }
    public Student Student { get; set; }
    
}