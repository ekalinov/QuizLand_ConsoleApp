namespace Quiz.ConsoleUI.Services
{
    public  interface IMainMenuService
    {
        

        void RunInteractiveMenu();
             
        void ChooseQuiz();
             
        void ConfirmStartRandomQuiz();
             
        void Results();
             
        void Settings();
    }
}
