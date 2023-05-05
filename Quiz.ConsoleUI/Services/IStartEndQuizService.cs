namespace Quiz.ConsoleUI.Services
{
    public interface IStartEndQuizService
    {

        void StartQuiz(string quizName);

        void StartRandomQuiz();

        void EndQuiz(string result);

    }
}
