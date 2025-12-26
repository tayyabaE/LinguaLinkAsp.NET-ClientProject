using Microsoft.EntityFrameworkCore;
using Web_Based_Learning_System.Controllers;
using Web_Based_Learning_System.Models;

namespace Web_Based_Learning_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set;}
        public DbSet<Pronunciation> Pronunciations { get; set; }
        public DbSet<UserProgress> UserProgress { get; set; }
    }
}
