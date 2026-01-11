using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Based_Learning_System.Data;
using Web_Based_Learning_System.Models;
using Web_Based_Learning_System.ViewModels;

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

        public IActionResult ManageUsers(string status)
        {
            var usersQuery = _context.Users.AsQueryable();
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);

            if (status == "active")
            {
                usersQuery = usersQuery.Where(u =>
                    u.LastLoginAt != null && u.LastLoginAt >= oneYearAgo);
            }
            else if (status == "inactive")
            {
                usersQuery = usersQuery.Where(u =>
                    u.LastLoginAt == null || u.LastLoginAt < oneYearAgo);
            }

            ViewBag.ActiveCount = _context.Users.Count(u =>
                u.LastLoginAt != null && u.LastLoginAt >= oneYearAgo);

            ViewBag.InactiveCount = _context.Users.Count(u =>
                u.LastLoginAt == null || u.LastLoginAt < oneYearAgo);

            ViewBag.CurrentStatus = status; 

            return View(usersQuery.ToList());
        }



        public IActionResult ManageCourses()
        {
            var courses = _context.Courses.Include(c => c.Lessons).ToList();
            return View(courses);
        }
        
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult AddUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(model);
            }

            string profilePath = "/images/default-avatar.png";

            // Handle profile picture upload
            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid() + Path.GetExtension(model.ProfilePicture.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(stream);
                }

                profilePath = "/uploads/" + fileName;
            }

            var user = new User
            {
                FullName = model.FullName,
                Nickname = model.Nickname,
                Email = model.Email,
                Role = model.Role,
                PasswordHash = HashPassword(model.Password),
                ProfilePicturePath = profilePath,
                LastLoginAt = null
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "User added successfully!";
            return RedirectToAction("AddUser");
        }

        public IActionResult AddUser()
        {
            var model = new UserViewModel();
            return View(model); 
        }


        private string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
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
