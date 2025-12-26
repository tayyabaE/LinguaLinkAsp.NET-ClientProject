using System.ComponentModel.DataAnnotations.Schema;
using Web_Based_Learning_System.Models;

namespace Web_Based_Learning_System.Controllers
{
    public class Vocabulary
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }
        public string Example { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }

}
