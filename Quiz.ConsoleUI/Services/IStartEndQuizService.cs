namespace Quiz.ConsoleUI.Services
{
    public interface IStartEndQuizService
    {

        void ChooseQuiz();

        void StartQuiz(string quizName);

        void StartRandomQuiz();

        void EndQuiz(string result);

        public void ConfirmStartQuiz(string quizName, string quizDescrioption);

        public void ConfirmStartRandomQuiz();


    }
}
