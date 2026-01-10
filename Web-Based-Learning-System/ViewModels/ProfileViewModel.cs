namespace Web_Based_Learning_System.ViewModels
{
    public class ProfileViewModel
    {
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }

        public IFormFile ProfilePicture { get; set; }
        public string ProfilePicturePath { get; set; }
    }

}
