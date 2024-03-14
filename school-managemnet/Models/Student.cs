using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolManagement.Models
{
    public class Student : ApplicationUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int SId { get; set; }

        [DataType(DataType.EmailAddress)]
        [Remote("CheckEmail", "Student", AdditionalFields = "Id", ErrorMessage = "Email alraedy exists.")]
        public override string Email { get; set; }
        public ICollection<Course> Courses { get; set; }

        public int DepartemntId { get; set; }

        [ForeignKey(nameof(DepartemntId))]
        public Department Department { get; set; }
    }
}
