using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;
using SchoolManagement.ViewModels;
using System.Security.Claims;

namespace SchoolManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController
            (UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _SignInManager)
        {
            userManager = _userManager;
            signInManager = _SignInManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                //create account
                ApplicationUser userModel = new ApplicationUser()
                {
                    UserName = newUserVM.UserName,
                    PasswordHash = newUserVM.Password,
                    Address = newUserVM.Address
                };


                IdentityResult result = await userManager.CreateAsync(userModel, newUserVM.Password);
                if (result.Succeeded)
                {
                    //Assign Role
                    await userManager.AddToRoleAsync(userModel, "Admin");
                    //create cookie
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(newUserVM);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel UserVM)
        {
            if (ModelState.IsValid)
            {
                var userModel = await userManager.FindByNameAsync(UserVM.UserName);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, UserVM.Password);
                    if (found)
                    {
                        //await signInManager.SignInAsync(userModel, UserVM.RememberMe);
                        List<Claim> Claims = [new("Address", userModel.Address)];

                        await signInManager.SignInWithClaimsAsync(
                        userModel,
                        UserVM.RememberMe,
                        Claims
                        );
                        return RedirectToAction("Index", "Student");
                    }
                }
                ModelState.AddModelError("", "Inavalid username or password.");

            }
            return View(UserVM);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVM)
        {

            if (ModelState.IsValid)
            {
                //create account
                ApplicationUser userModel = new ApplicationUser()
                {
                    UserName = newUserVM.UserName,
                    PasswordHash = newUserVM.Password,
                    Address = newUserVM.Address
                };


                IdentityResult result = await userManager.CreateAsync(userModel, newUserVM.Password);
                if (result.Succeeded)
                {
                    //create cookie
                    await signInManager.SignInAsync(userModel, false);
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(newUserVM);
        }



        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


    }


}
