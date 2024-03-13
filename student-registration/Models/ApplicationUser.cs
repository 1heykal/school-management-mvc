using Microsoft.AspNetCore.Identity;

namespace SchoolManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }

    }
}
