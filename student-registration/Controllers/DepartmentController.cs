using student_registration.BLL;
using student_registration.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace student_registration.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        IDepartment db;

        public DepartmentController(IDepartment _db)
        {
            db = _db;
        }

   
        public IActionResult Details(Department department)
        {
            Department dept = db.GetByID(department.Id);
            return View(dept);
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                db.Add(department);
                return RedirectToAction("Index");
            }

            return View();

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {

            if (ModelState.IsValid)
            {
                db.Edit(department);
                return RedirectToAction("Details", department);
            }

            return View(department);

        }
        public IActionResult Edit(int id)
        {
            return View(db.GetByID(id));
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = db.GetAll();
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(Department department)
        {
            db.Delete(department.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
          
            return View(db.GetByID(id.Value));
        }

    }
}
