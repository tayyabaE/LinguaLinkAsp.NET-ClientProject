using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Based_Learning_System.Models
{
    public class UserProgress
    {
        [Key]
        public int Id { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? CompletedDate { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int LessonId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }
    }
}
