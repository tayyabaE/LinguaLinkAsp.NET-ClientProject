using System.ComponentModel.DataAnnotations;

namespace Web_Based_Learning_System.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        public string Role { get; set; } // e.g., Admin, Instructor, Learner
        public string Password { get; set; } = string.Empty; 
    }
}
