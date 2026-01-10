using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Based_Learning_System.Data;
using Web_Based_Learning_System.Models;

namespace Web_Based_Learning_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Courses()
        {
            var courses = _context.Courses.ToList(); 
            return View(courses);                    
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        public IActionResult Preview(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();

            var model = new CoursePreviewViewModel
            {
                Course = course,
                Lessons = _context.Lessons
                    .Where(l => l.CourseId == id)
                    .OrderBy(l => l.OrderNo)
                    .ToList(),

                Quizzes = _context.Quizzes
                    .Where(q => q.CourseId == id)
                    .ToList(),

                //Vocabularies = _context.Vocabularies
                //    .Where(v => v.CourseId == id)
                //    .ToList(),

                //Pronunciations = _context.Pronunciations
                //    .Where(p => p.CourseId == id)
                //    .ToList()
            };

            return View(model);
        }


    }
}
