using SchoolManagement.BLL;
using SchoolManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace SchoolManagement.Controllers
{
    [Authorize]
    public class StudentController(IStudent db, ILogger<StudentController> logger) : Controller
    {
        private readonly ILogger<StudentController> _logger = logger;

        // [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            Student student = await db.GetById(id);
            return View(student);
        }

        public async Task<IActionResult> DisplayEdited()
        {
            int id = HttpContext.Session.GetInt32("Id")?? 0;
            if (id != null)
            {
                Student stu = await db.GetById(id);
                return View(stu);
            }
            return Content("No id provided");
        }

        public async Task<IActionResult> DisplayCreated()
        {

            //  if ((Request.Cookies["CId"]?? 0) is int id)
            {
               // Student st = await db.GetById(id);
                return View(); // st
            }


            return Content("No id provided");
        }

        public async Task<IActionResult> CheckEmail(string email, int id)
        {
            List<Student> result = await db.GetAll();

            if (result.Count == 0 || (result.Count == 1 && result[0].Id == id))
            {
                return Json(true);
            }

            return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student)
        {

            if (ModelState.IsValid)
            {
                await db.Add(student);
                Response.Cookies.Append("CId", student.Id + "");
                Response.Cookies.Append("CName", student.FirstName);
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
        public async Task<IActionResult> Edit(Student model)
        {
            var student = await db.GetById(model.Id);
            if(student is null)
                return NotFound();
                
            if (!ModelState.IsValid)
                return View(model);
            
            await db.Edit(model);
            
            HttpContext.Session.SetInt32("Id", model.Id);
            HttpContext.Session.SetString("Name", model.FirstName);
            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();
            
            var student = await db.GetById(id.Value);

            if (student is null)
                return NotFound();
            
            return View(student);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var s = User.Identity?.Name;
            var model = await db.GetAll();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student student)
        {
            await db.Delete(student.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await db.GetById(id));
        }
        
    }
}
