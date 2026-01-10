namespace Web_Based_Learning_System.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public string SelectedAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime AttemptDate { get; set; }
        public bool IsReviewed { get; set; } = false;
    }

}
