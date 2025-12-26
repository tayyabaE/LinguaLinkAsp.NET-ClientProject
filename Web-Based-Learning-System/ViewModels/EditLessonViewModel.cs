using Web_Based_Learning_System.Models;
using System.Collections.Generic;

namespace Web_Based_Learning_System.ViewModels
{
    public class EditLessonViewModel
    {
        public Lesson Lesson { get; set; }
        public List<Course> Courses { get; set; }
    }
}
