using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Based_Learning_System.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        public DateTime EnrolledDate { get; set; } = DateTime.Now;

        // Foreign Keys
        public int UserId { get; set; }
        public int CourseId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}
