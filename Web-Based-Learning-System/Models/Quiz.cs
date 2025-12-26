using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Based_Learning_System.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string OptionA { get; set; }

        [Required]
        public string OptionB { get; set; }

        [Required]
        public string OptionC { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }

        // FK
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}
