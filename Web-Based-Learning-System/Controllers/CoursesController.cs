using Microsoft.AspNetCore.Mvc;
using Web_Based_Learning_System.Data;
using Web_Based_Learning_System.Models;

namespace Web_Based_Learning_System.Controllers
{
        public class CoursesController : Controller
        {
            private readonly ApplicationDbContext _context;

            public CoursesController(ApplicationDbContext context)
            {
                _context = context;
            }

       


        public IActionResult Details(int id)
            {
                var course = _context.Courses.FirstOrDefault(c => c.Id == id);
                if (course == null) return NotFound();
                return View(course);
            }
        //public IActionResult AddCourse()
        //{
        //    return View("~/Views/Instructor/AddCourse.cshtml");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddCourse(Course course)
        //{
        //    // Force adding to DB
        //    _context.Courses.Add(course);
        //    _context.SaveChanges();
        //    TempData["SuccessMessage"] = "Course added successfully!";
        //    return RedirectToAction("AddCourse");
        //}



    }

}
