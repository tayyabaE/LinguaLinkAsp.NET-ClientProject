using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Web_Based_Learning_System.Data;
using Web_Based_Learning_System.Models;

namespace Web_Based_Learning_System.Controllers
{
        public class AccountController : Controller
        {
            private readonly ApplicationDbContext _context;

            public AccountController(ApplicationDbContext context)
            {
                _context = context;
            }

            // REGISTER
            public IActionResult Register()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Register(User user, string password)
            {
                user.PasswordHash = HashPassword(password);
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            // LOGIN
            public IActionResult Login()
            {
                return View();
            }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string hash = HashPassword(password);
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.PasswordHash == hash);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString()); 
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserName", user.FullName);

                if (user.Role == "Admin")
                    return RedirectToAction("Index", "Admin");

                if (user.Role == "Instructor")
                    return RedirectToAction("Index", "Instructor");

                return RedirectToAction("Index", "Learner");
            }

            ViewBag.Error = "Invalid login";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



            // PASSWORD HASHING
            private string HashPassword(string password)
            {
                using var sha = SHA256.Create();
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

}
