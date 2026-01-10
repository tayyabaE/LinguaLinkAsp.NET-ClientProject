using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Based_Learning_System.Models;

namespace Web_Based_Learning_System.Controllers
{
    public class Vocabulary
    {
        [Key]
        public int Id { get; set; }

        // 🔹 NEW Foreign Key
        public int LessonId { get; set; }

        public string Word { get; set; }
        public string EnglishMeaning { get; set; }
        public string NativeMeaning { get; set; }
        public string EnglishExample { get; set; }
        public string NativeExample { get; set; }
        public string Emoji { get; set; }

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }
    }


}
