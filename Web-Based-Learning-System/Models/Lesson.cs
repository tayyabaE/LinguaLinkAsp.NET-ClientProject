using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Based_Learning_System.Controllers;

namespace Web_Based_Learning_System.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int OrderNo { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        // 🔹 ADD THIS
        public ICollection<Vocabulary> Vocabularies { get; set; }
    }

}
