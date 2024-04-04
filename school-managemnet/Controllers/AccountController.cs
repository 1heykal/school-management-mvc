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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController
            (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterUserViewModel newUserVm)
        {
            if (ModelState.IsValid)
            {
                //create account
                ApplicationUser userModel = new ApplicationUser()
                {
                    UserName = newUserVm.UserName,
                    PasswordHash = newUserVm.Password,
                    Address = newUserVm.Address
                };


                IdentityResult result = await _userManager.CreateAsync(userModel, newUserVm.Password);
                if (result.Succeeded)
                {
                    //Assign Role
                    await _userManager.AddToRoleAsync(userModel, "Admin");
                    //create cookie
                    await _signInManager.SignInAsync(userModel, false);
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
            return View(newUserVm);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userVm)
        {
            if (ModelState.IsValid)
            {
                var userModel = await _userManager.FindByNameAsync(userVm.UserName);
                if (userModel != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(userModel, userVm.Password);
                    if (found)
                    {
                        //await signInManager.SignInAsync(userModel, UserVM.RememberMe);
                        List<Claim> claims = [new("Address", userModel.Address)];

                        await _signInManager.SignInWithClaimsAsync(
                        userModel,
                        userVm.RememberMe,
                        claims
                        );
                        return RedirectToAction("Index", "Student");
                    }
                }
                ModelState.AddModelError("", "Inavalid username or password.");

            }
            return View(userVm);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVm)
        {

            if (ModelState.IsValid)
            {
                //create account
                ApplicationUser userModel = new ApplicationUser()
                {
                    UserName = newUserVm.UserName,
                    PasswordHash = newUserVm.Password,
                    Address = newUserVm.Address
                };


                IdentityResult result = await _userManager.CreateAsync(userModel, newUserVm.Password);
                if (result.Succeeded)
                {
                    //create cookie
                    await _signInManager.SignInAsync(userModel, false);
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
            return View(newUserVm);
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


    }


}
