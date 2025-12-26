using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Web_Based_Learning_System.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Level is required.")]
        public string Level { get; set; } // e.g., Beginner, Intermediate, Advanced

        public ICollection<Lesson> Lessons { get; set; }
    }
}