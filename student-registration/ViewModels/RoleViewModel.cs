using System.ComponentModel.DataAnnotations;

namespace student_registration.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }


    }
}
