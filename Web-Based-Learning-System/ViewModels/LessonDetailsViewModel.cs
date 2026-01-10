using Web_Based_Learning_System.Controllers;
using Web_Based_Learning_System.Models;

public class LessonDetailsViewModel
{
    public Lesson Lesson { get; set; }
    public List<Vocabulary> Vocabularies { get; set; }
    public List<Pronunciation> Pronunciations { get; set; }
}
