using System.ComponentModel.DataAnnotations;

namespace Web_Based_Learning_System.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } // Learner, Instructor, Admin

        // New fields
        public string Nickname { get; set; }
        public string ProfilePicturePath { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }

}
