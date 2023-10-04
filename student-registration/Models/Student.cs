using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace student_registration.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30)]
        [Required]
        public string Name { get; set; }

        [Range(18, 80)]
        public int Age { get; set; }


        [DataType(DataType.Password)]
        [Required]
        [MinLength(3)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string CPassword { get; set; }


        public int? Dno { get; set; }

        [DataType(DataType.EmailAddress)]
        [Remote("CheckEmail", "Student", AdditionalFields = "Id", ErrorMessage = "Email alraedy exists")]

        public string Email { get; set; }

        [ForeignKey("Dno")]
        public Department Department { get; set; }
    }
}
