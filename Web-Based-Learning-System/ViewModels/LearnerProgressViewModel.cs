using System;
using System.Collections.Generic;
using Web_Based_Learning_System.Models;

namespace Web_Based_Learning_System.ViewModels
{
    public class LearnerProgressViewModel
    {
        public List<UserProgress> CompletedLessons { get; set; }
        public List<QuizAttemptWithQuiz> QuizAttempts { get; set; }
    }

    public class QuizAttemptWithQuiz
    {
        public int QuizId { get; set; }
        public string Question { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime AttemptDate { get; set; }
    }
}
