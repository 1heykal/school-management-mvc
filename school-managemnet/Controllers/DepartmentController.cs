using SchoolManagement.BLL;
using SchoolManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SchoolManagement.Controllers
{
    //[Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartment _db;

        public DepartmentController(IDepartment db)
        {
            this._db = db;
        }


        public IActionResult Details(Department department)
        {
            Department dept = _db.GetById(department.Id);
            return View(dept);
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                _db.Add(department);
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
                _db.Edit(department);
                return RedirectToAction("Details", department);
            }

            return View(department);

        }
        public IActionResult Edit(int id)
        {
            return View(_db.GetById(id));
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = _db.GetAll();
            return View(model);
        }


        [HttpPost]
        public IActionResult Delete(Department department)
        {
            _db.Delete(department.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {

            return View(_db.GetById(id.Value));
        }

    }
}
