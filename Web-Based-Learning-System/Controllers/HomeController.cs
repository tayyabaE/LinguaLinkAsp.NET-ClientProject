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
            // Get the course
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();

            // Get lessons for this course
            var lessons = _context.Lessons
                .Where(l => l.CourseId == id)
                .OrderBy(l => l.OrderNo)
                .ToList();

            // Get quizzes for this course
            var quizzes = _context.Quizzes
                .Where(q => q.CourseId == id)
                .ToList();

            // Get vocabularies for this course (all vocabularies from its lessons)
            var vocabularies = _context.Vocabularies
                .Where(v => lessons.Select(l => l.Id).Contains(v.LessonId))
                .ToList();

            // Get pronunciations if you have a Pronunciation table
            var pronunciations = _context.Pronunciations
                .Where(p => lessons.Select(p => p.Id).Contains(p.LessonId))
                .ToList();

            // Build the view model
            var model = new CoursePreviewViewModel
            {
                Course = course,
                Lessons = lessons,
                Quizzes = quizzes,
                Vocabularies = vocabularies,
                Pronunciations = pronunciations
            };

            return View(model);
        }

    }
}
