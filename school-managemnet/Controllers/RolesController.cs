using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Controllers
{
    [Authorize(Roles = "Admin")] // [Authorize(Roles = "Admin,Student")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager) 
        {
            this._roleManager = roleManager;
        }

        // Create Role
        // Link
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        // Submit
        [HttpPost]
        public async Task<IActionResult> New(RoleViewModel newRoleVm)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new();
                role.Name = newRoleVm.RoleName;
                IdentityResult result = await _roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return View(new RoleViewModel());
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }
           
            return View(newRoleVm);
        }
    }
}

