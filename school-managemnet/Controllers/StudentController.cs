using SchoolManagement.BLL;
using SchoolManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace SchoolManagement.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudent _db;


        public StudentController(IStudent db)
        {
            _db = db;
        }

        // [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            Student student = await _db.GetById(id);
            return View(student);
        }

        public async Task<IActionResult> DisplayEdited()
        {
            int id = HttpContext.Session.GetInt32("Id")?? 0;
            if (id != null)
            {
                Student stu = await _db.GetById(id);
                return View(stu);
            }
            return Content("No id provided");
        }

        public async Task<IActionResult> DisplayCreated()
        {

            //  if ((Request.Cookies["CId"]?? 0) is int id)
            {
               // Student st = await _db.GetById(id);
                return View(); // st
            }


            return Content("No id provided");
        }

        public async Task<IActionResult> CheckEmail(string email, int id)
        {
            List<Student> result = await _db.GetAll();

            if (result.Count == 0 || (result.Count == 1 && result[0].Id == id))
            {
                return Json(true);
            }

            return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {

            if (ModelState.IsValid)
            {
                await _db.Add(student);
                Response.Cookies.Append("CId", student.Id + "");
                Response.Cookies.Append("CName", student.FirstName);
                return RedirectToAction("Index");
            }

            // ViewBag.Courses = new SelectList(_departmentBLL.GetAll(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
          //  ViewBag.departments = new SelectList(_departmentBLL.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {

            if (ModelState.IsValid)
            {
                _db.Edit(student);
                HttpContext.Session.SetInt32("Id", student.Id);
                HttpContext.Session.SetString("Name", student.FirstName);
                return RedirectToAction("Index");
            }
            // ViewBag.departments = new SelectList(_departmentBLL.GetAll(), "Id", "Name");
            return View(student);


        }
        public async Task<IActionResult> Edit(int id)
        {
            Student st = await _db.GetById(id);
            // ViewBag.departments = new SelectList(_departmentBLL.GetAll(), "Id", "Name");
            return View(st);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var s = User.Identity?.Name;
            var model = await _db.GetAll();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student student)
        {
            await _db.Delete(student.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _db.GetById(id));
        }



    }
}
