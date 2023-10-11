using student_registration.BLL;
using student_registration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace student_registration.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        IStudent db;
        

        public StudentController(IStudent _db)
        {
            db = _db;
        }

        [Authorize]
        public IActionResult Details(Student student)
        {   
            Student stu = db.GetByID(student.Id);
            return View(stu);
        }

        public IActionResult DisplayEdited()
        {
            int? id = HttpContext.Session.GetInt32("Id");
            if(id != null)
            {
                Student stu = db.GetByID(id.Value);
                return View(stu);
            }
            return Content("No id provided");
        }

        public IActionResult DisplayCreated()
        {
            int id = 0;
            
            if (int.TryParse(Request.Cookies["CId"], out id))
            {
                Student st = db.GetByID(id);
                return View(st);
            }

            
            return Content("No id provided");
        }

        public IActionResult CheckEmail(string email, int id)
        {
           List<Student>  result = db.GetAll().Where(s => s.Email == email).ToList();

            if(result.Count == 0 || (result.Count == 1 && result[0].Id == id))
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
                db.Add(student);
                Response.Cookies.Append("CId", student.Id + "");
                Response.Cookies.Append("CName", student.Name);
                return RedirectToAction("Index");
            }

            DepartmentBLL depts = new();
            ViewBag.departments = new SelectList(depts.GetAll(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            DepartmentBLL depts = new();
            ViewBag.departments = new SelectList(depts.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {

            if (ModelState.IsValid)
            {
                db.Edit(student);
                HttpContext.Session.SetInt32("Id", student.Id);
                HttpContext.Session.SetString("Name", student.Name);
                return RedirectToAction("Index");
            }
            DepartmentBLL depts = new();
            ViewBag.departments = new SelectList(depts.GetAll(), "Id", "Name");
            return View(student);


        }
        public IActionResult Edit(int id)
        {
            Student st = db.GetByID(id);
            DepartmentBLL depts = new();
            ViewBag.departments = new SelectList(depts.GetAll(), "Id", "Name");
            return View(st);
        }

        public IActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Student student)
        {
      
            db.Delete(student.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            return View(db.GetByID(id.Value));
        }



    }
}
