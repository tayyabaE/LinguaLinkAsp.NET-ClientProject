using Web_Based_Learning_System.Controllers;
using Web_Based_Learning_System.Models;

public class CourseDetailsViewModel
{
    public Course Course { get; set; }
    public List<Lesson> Lessons { get; set; }
    public List<Quiz> Quizzes { get; set; }
    public List<Vocabulary> Vocabularies { get; set; }
    public List<Pronunciation> Pronunciations { get; set; }

}
