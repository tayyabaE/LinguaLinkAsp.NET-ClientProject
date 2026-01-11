using System.ComponentModel.DataAnnotations;

namespace Web_Based_Learning_System.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public string Nickname { get; set; }

        public IFormFile ProfilePicture { get; set; }
    }
}
