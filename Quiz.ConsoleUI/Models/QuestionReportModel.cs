namespace Quiz.ConsoleUI.Models
{
    public class QuestionReportModel
    {

        public string QuestionTitle { get; set; } = null!;

        public string CorrectAnswerTitle { get; set; } = null!;

        public string? UserAnswerTitle { get; set; }

    }
}