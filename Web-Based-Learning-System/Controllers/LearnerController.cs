using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Based_Learning_System.Data;
using Web_Based_Learning_System.Models;
using Web_Based_Learning_System.ViewModels;

namespace Web_Based_Learning_System.Controllers
{

    public class LearnerController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public LearnerController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            return View();
        }



        public IActionResult Lessons(int courseId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);

            var isEnrolled = _context.Enrollments
                .Any(e => e.UserId == userId && e.CourseId == courseId);

            if (!isEnrolled)
                return Unauthorized(); // or RedirectToAction("AllCourses")

            var lessons = _context.Lessons
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.OrderNo)
                .ToList();

            // Get completed lessons for this user
            var completedLessons = _context.UserProgress
                .Where(p => p.UserId == userId && p.IsCompleted)
                .Select(p => p.LessonId)
                .ToList();

            ViewBag.CompletedLessons = completedLessons;

            return View(lessons);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteLesson(int lessonId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);

            // Check if already marked completed
            var alreadyCompleted = _context.UserProgress
                .Any(p => p.UserId == userId && p.LessonId == lessonId);

            if (!alreadyCompleted)
            {
                _context.UserProgress.Add(new UserProgress
                {
                    UserId = userId,
                    LessonId = lessonId,
                    IsCompleted = true,
                    CompletedDate = DateTime.Now
                });

                _context.SaveChanges();
                TempData["Success"] = "Lesson marked as completed ";
            }

           
            var lesson = _context.Lessons.Find(lessonId);
            if (lesson == null)
                return RedirectToAction("CourseDetails");

            return RedirectToAction("CourseDetails", "Learner", new { courseId = lesson.CourseId });
        }



public IActionResult Progress()
    {
        int userId = int.Parse(HttpContext.Session.GetString("UserId"));

        var completedLessons = _context.UserProgress
            .Where(p => p.UserId == userId)
            .Include(p => p.Lesson)
            .ToList();

        var quizAttempts = _context.QuizAttempts
            .Where(q => q.UserId == userId)
            .Join(_context.Quizzes,
                  qa => qa.QuizId,
                  q => q.Id,
                  (qa, q) => new QuizAttemptWithQuiz
                  {
                      QuizId = q.Id,
                      Question = q.Question,
                      IsCorrect = qa.IsCorrect,
                      AttemptDate = qa.AttemptDate
                  })
            .ToList();

        var vm = new LearnerProgressViewModel
        {
            CompletedLessons = completedLessons,
            QuizAttempts = quizAttempts
        };

        return View(vm);
    }



    [HttpGet]
        public IActionResult Profile()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);

            var user = _context.Users.Find(userId);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var model = new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email
            };

            return View(model);
        }


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

            var user = _context.Users.Find(userId);
            if (user == null)
                return RedirectToAction("Login", "Account");

            user.FullName = model.FullName;
            user.Email = model.Email;

            _context.SaveChanges();

            TempData["Success"] = "Profile updated successfully ✅";

            return RedirectToAction("Profile");
        }

        public IActionResult ExploreCourses()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);

            var courses = _context.Courses
                .Select(c => new LearnerCourseViewModel
                {
                    CourseId = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Level = c.Level,
                    IsEnrolled = _context.Enrollments
                        .Any(e => e.UserId == userId && e.CourseId == c.Id)
                })
                .ToList();

            return View(courses);
        }


        public IActionResult MyCourses()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);

            var courses = _context.Enrollments
                .Where(e => e.UserId == userId)
                .Select(e => e.Course)
                .ToList();

            return View(courses);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enroll(int courseId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdString);

            var alreadyEnrolled = _context.Enrollments
                .Any(e => e.UserId == userId && e.CourseId == courseId);

            if (!alreadyEnrolled)
            {
                _context.Enrollments.Add(new Enrollment
                {
                    UserId = userId,
                    CourseId = courseId,
                    EnrolledDate = DateTime.Now   
                });

                _context.SaveChanges();
            }

            return RedirectToAction("MyCourses");
        }

        public IActionResult CourseDetails(int courseId)
        {
            var course = _context.Courses
                                 .Include(c => c.Lessons)
                                 .FirstOrDefault(c => c.Id == courseId);

            var quizzes = _context.Quizzes
                                  .Where(q => q.CourseId == courseId)
                                  .ToList();

            var vm = new CourseDetailsViewModel
            {
                Course = course,
                Lessons = course.Lessons.OrderBy(l => l.OrderNo).ToList(),
                Quizzes = quizzes
            };

            return View(vm);
        }

        public IActionResult LessonDetails(int lessonId)
        {
            var lesson = _context.Lessons.FirstOrDefault(l => l.Id == lessonId);
            if (lesson == null) return NotFound();

            return View(lesson);
        }

        public IActionResult AttemptQuiz(int quizId)
        {
            var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == quizId);
            if (quiz == null)
                return NotFound();

            return View(quiz);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitQuiz(int quizId, string answer)
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            var quiz = _context.Quizzes.Find(quizId);
            if (quiz == null) return NotFound();

            bool isCorrect = quiz.CorrectAnswer == answer;

            _context.QuizAttempts.Add(new QuizAttempt
            {
                UserId = userId,
                QuizId = quizId,
                SelectedAnswer = answer,
                IsCorrect = isCorrect,
                AttemptDate = DateTime.Now
            });

            _context.SaveChanges();

            TempData["Success"] = isCorrect ? "Correct Answer 🎉" : "Wrong Answer ❌";

            return RedirectToAction("Progress");
        }


    }
}
