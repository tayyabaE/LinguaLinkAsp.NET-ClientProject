namespace Web_Based_Learning_System.Models
{
    public class LearnerCourseViewModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public bool IsEnrolled { get; set; }
    }

}
