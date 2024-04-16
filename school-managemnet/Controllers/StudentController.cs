using SchoolManagement.BLL;
using SchoolManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Controllers
{
    [Authorize]
    public class StudentController(IStudent db, ILogger<StudentController> logger) : Controller
    {
        
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        private string ErrorMessage { get; set; }
        public async Task<IActionResult> Details(int? id)
        {
            if(id is null)
                return NotFound();
            
            var student = await db.GetById(id.Value);
            return View(student);
        }

        public async Task<IActionResult> DisplayEdited()
        {
            int id = HttpContext.Session.GetInt32("Id")?? 0;
            if (id != 0)
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
            List<Student> result = await db.GetAll().ToListAsync();

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
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            
            var s = User.Identity?.Name;

            ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSort"] = sortOrder == "Date" ? "date_desc" : "Date";
            var students =  db.GetAll();


            students = sortOrder switch
            {
                "name_desc" => students.OrderByDescending(student => student.LastName),
                "Date" => students.OrderBy(student => student.EnrollmentDate),
                "date_desc" => students.OrderByDescending(student => student.EnrollmentDate),
                _ => students.OrderBy(student => student.LastName)
            };
            
            return View(await students.AsNoTracking().ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();
            
            var student = await db.GetById(id.Value);

            if (student is null)
                return NotFound();

            try
            {
                await db.Delete(student.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, ErrorMessage);
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }

        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if(id is null)
                return NotFound();

            var student = await db.GetById(id.Value);

            if (student is null)
                return NotFound();

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = $"Delete {id} failed. Try again";
                ViewData["ErrorMessage"] = ErrorMessage;
            }
            return View(student);
        }
        
    }
}
