using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Based_Learning_System.Data;
using Web_Based_Learning_System.Models;

namespace Web_Based_Learning_System.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalCourses = _context.Courses.Count(),
                TotalLessons = _context.Lessons.Count(),
                TotalUsers = _context.Users.Count(),
                TotalInstructors = _context.Users.Count(u => u.Role == "Instructor"),
                TotalLearners = _context.Users.Count(u => u.Role == "Learner")
            };

            return View(model);
        }

        public IActionResult ManageUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult ManageCourses()
        {
            var courses = _context.Courses.Include(c => c.Lessons).ToList();
            return View(courses);
        }
        // Add User - GET
        public IActionResult AddUser()
        {
            var model = new UserViewModel();
            return View(model);
        }

        // Add User - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Role = model.Role,
                PasswordHash = model.Password // or hash it properly
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "User added successfully!";
            return RedirectToAction("ManageUsers");
        }


        public IActionResult EditUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            var model = new UserViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };

            return View(model);
        }

        // Edit User - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.Find(model.Id);
            if (user == null) return NotFound();

            user.FullName = model.FullName;
            user.Email = model.Email;
            user.Role = model.Role;

            _context.SaveChanges();

            TempData["Success"] = "User updated successfully!";
            return RedirectToAction("ManageUsers");
        }

        // Delete User
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction("ManageUsers");
        }
        // GET
        public IActionResult AddCourse()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(Course model)
        {

            _context.Courses.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Course added successfully!";
            return RedirectToAction("ManageCourses");
        }
        
        public IActionResult DeleteCourse(int id)
        {
            var course = _context.Courses
                .Include(c => c.Lessons)
                .FirstOrDefault(c => c.Id == id);

            if (course == null)
                return NotFound();

            // Remove lessons first (important)
            _context.Lessons.RemoveRange(course.Lessons);

            _context.Courses.Remove(course);
            _context.SaveChanges();

            TempData["Success"] = "Course deleted successfully!";
            return RedirectToAction("ManageCourses");
        }
        // GET
        public IActionResult EditCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course == null) return NotFound();

            return View(course);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCourse(Course model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var course = _context.Courses.Find(model.Id);
            if (course == null) return NotFound();

            course.Name = model.Name;
            course.Level = model.Level;
            course.Description = model.Description;

            _context.SaveChanges();

            TempData["Success"] = "Course updated successfully!";
            return RedirectToAction("ManageCourses");
        }



    }
}
