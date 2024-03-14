using SchoolManagement.BLL;
using SchoolManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SchoolManagement.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudent _db;
        private readonly IDepartment _departmentBLL;


        public StudentController(IStudent db, IDepartment departmentBLL)
        {
            _db = db;
            _departmentBLL = departmentBLL;
        }

        // [Authorize]
        public IActionResult Details(Student student)
        {
            Student stu = _db.GetByID(student.SId);
            return View(stu);
        }

        public IActionResult DisplayEdited()
        {
            int? id = HttpContext.Session.GetInt32("Id");
            if (id != null)
            {
                Student stu = _db.GetByID(id.Value);
                return View(stu);
            }
            return Content("No id provided");
        }

        public IActionResult DisplayCreated()
        {

            if (int.TryParse(Request.Cookies["CId"], out int id))
            {
                Student st = _db.GetByID(id);
                return View(st);
            }


            return Content("No id provided");
        }

        public IActionResult CheckEmail(string email, int id)
        {
            List<Student> result = _db.GetAll().Where(s => s.Email == email).ToList();

            if (result.Count == 0 || (result.Count == 1 && result[0].SId == id))
            {
                return Json(true);
            }

            return Json(false);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {

            if (ModelState.IsValid)
            {
                _db.Add(student);
                Response.Cookies.Append("CId", student.Id + "");
                Response.Cookies.Append("CName", student.FirstName);
                return RedirectToAction("Index");
            }

            ViewBag.departments = new SelectList(_departmentBLL.GetAll(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.departments = new SelectList(_departmentBLL.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {

            if (ModelState.IsValid)
            {
                _db.Edit(student);
                HttpContext.Session.SetInt32("Id", student.SId);
                HttpContext.Session.SetString("Name", student.FirstName);
                return RedirectToAction("Index");
            }
            ViewBag.departments = new SelectList(_departmentBLL.GetAll(), "Id", "Name");
            return View(student);


        }
        public IActionResult Edit(int id)
        {
            Student st = _db.GetByID(id);
            ViewBag.departments = new SelectList(_departmentBLL.GetAll(), "Id", "Name");
            return View(st);
        }

        [Authorize]
        public IActionResult Index()
        {
            var s = User.Identity.Name;
            var model = _db.GetAll();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Student student)
        {
            _db.Delete(student.SId);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            return View(_db.GetByID(id.Value));
        }



    }
}
