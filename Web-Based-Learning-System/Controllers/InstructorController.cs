using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Based_Learning_System.Data;
using Web_Based_Learning_System.Models;
using Web_Based_Learning_System.ViewModels;

namespace Web_Based_Learning_System.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
    {
        var courses = _context.Courses.Include(c => c.Lessons).ToList();
            LoadPendingNotifications();
            return View(courses);
    }

        public IActionResult Dashboard()
        {
            int pendingCount = _context.QuizAttempts
                .Where(q => q.IsReviewed == false)
                .Count();

            ViewBag.PendingNotifications = pendingCount;
            LoadPendingNotifications();

            return View();
        }

        public IActionResult AddCourse()
        {
            return View("AddCourse");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(Course course)
        {
           
            _context.Courses.Add(course);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Course added successfully!";
            return RedirectToAction("AddCourse");
        }





        // Add Lesson
        public IActionResult AddLesson()
        {
            ViewBag.Courses = _context.Courses.ToList();
            LoadPendingNotifications();
            return View("AddLesson");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Lesson added successfully!";
            return RedirectToAction("AddLesson");
        }


        // Edit Lesson
        public IActionResult EditLesson(int id)
    {
        var lesson = _context.Lessons.Find(id);
        if (lesson == null) return NotFound();

        var vm = new EditLessonViewModel
        {
            Lesson = lesson,
            Courses = _context.Courses.ToList()
        };
            LoadPendingNotifications();
            return View(vm);
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLesson(EditLessonViewModel vm)
        {
            
                _context.Lessons.Update(vm.Lesson);
                _context.SaveChanges();

              
                TempData["SuccessMessage"] = $"Lesson '{vm.Lesson.Title}' updated successfully!";

                return RedirectToAction("Index");
            

            vm.Courses = _context.Courses.ToList();
            return View(vm);
        }


        // Delete Lesson
        public IActionResult DeleteLesson(int? id)
        {
            if (id == null) return NotFound();

            var lesson = _context.Lessons.Include(l => l.Course).FirstOrDefault(l => l.Id == id);
            if (lesson == null) return NotFound();

            return View(lesson);
        }

        [HttpPost, ActionName("DeleteLesson")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLessonConfirmed(int id)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Profile()
        {
            // Get logged-in instructor ID from session
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);

            // Fetch instructor from database
            var instructor = _context.Users.Find(userId);
            if (instructor == null)
                return RedirectToAction("Login", "Account");

            var model = new ProfileViewModel
            {
                FullName = instructor.FullName,
                Email = instructor.Email
            };
            LoadPendingNotifications();
            return View(model);
        }
        private void LoadPendingNotifications()
        {
            ViewBag.PendingNotifications = _context.QuizAttempts
                .Where(q => q.IsReviewed == false)
                .Count();
        }
        public IActionResult Notifications()
        {
            LoadPendingNotifications();

            var pending = _context.QuizAttempts
                .Where(q => !q.IsReviewed)
                .OrderByDescending(q => q.AttemptDate)
                .ToList();

            return View(pending);
        }

        public IActionResult ReviewQuiz(int id)
        {
            var quiz = _context.QuizAttempts.Find(id);

            if (quiz != null)
            {
                quiz.IsReviewed = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Notifications");
        }



        // POST: Update Instructor Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(ProfileViewModel model)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);

            if (!ModelState.IsValid)
                return View(model);

            // Fetch instructor from database
            var instructor = _context.Users.Find(userId);
            if (instructor == null)
                return RedirectToAction("Login", "Account");

            // Update fields
            instructor.FullName = model.FullName;
            instructor.Email = model.Email;

            _context.SaveChanges();

            TempData["Success"] = "Profile updated successfully ";

            return RedirectToAction("Profile");
        }


    }
}
