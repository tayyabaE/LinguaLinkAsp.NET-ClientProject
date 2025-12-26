using Web_Based_Learning_System.Models;

public class CourseDetailsViewModel
{
    public Course Course { get; set; }
    public List<Lesson> Lessons { get; set; }
    public List<Quiz> Quizzes { get; set; }
}
