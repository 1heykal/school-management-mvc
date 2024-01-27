using Microsoft.AspNetCore.Identity;

namespace student_registration.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }

    }
}
